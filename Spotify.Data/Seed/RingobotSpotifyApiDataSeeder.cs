using GraphQLInDotNet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SpotifyApi.NetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SpotifyArtist = SpotifyApi.NetCore.Artist;
using SpotifyAlbum = SpotifyApi.NetCore.Album;
using SpotifyTrack = SpotifyApi.NetCore.Track;
using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using SearchApi = SpotifyApi.NetCore.SearchApi;

namespace Spotify.Data.Seed
{
    public class RingobotSpotifyApiDataSeeder : ISeeder
    {
        private bool seeded = false;
        private HttpClient httpClient;
        private AccountsService apiService;

        public RingobotSpotifyApiDataSeeder()
        {
            this.httpClient = new HttpClient();
            this.apiService = new AccountsService(httpClient, new SpotifyConfig());
        }

        public async Task SeedDataAsync(IDataContext dataContext)
        {
            if(seeded)
            {
                return;
            }

            seeded = true;

            await AddGenres(dataContext);
            await AddArtistsAsync(dataContext);
            await dataContext.SaveChangesAsync();
        }
        
        private async Task AddGenres(IDataContext dataContext)
        {
            var dataFile = Files.FIlesResource.genres;
            var names = dataFile.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var name in names)
            {
                await AddGenreIfNotExists(dataContext, name);
            }
        }

        private async Task AddGenreIfNotExists(IDataContext dataContext, string genreName)
        {
            if (dataContext.Genres.Query().Any(g => g.Name == genreName))
            {
                return;
            }

            dataContext.Genres.Add(Mapping.MapperHelper.MapToGenre(genreName));
        }

        private async Task AddArtistsAsync(IDataContext dataContext)
        {
            var search = new SpotifyApi.NetCore.SearchApi(this.httpClient, this.apiService);

            var bandsFile = Files.FIlesResource.bands;
            var artistNames = bandsFile.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var artistName in artistNames)
            {
                await AddArtistIfNotExistsAsync(dataContext, search, artistName);
            }
        }

        private async Task AddArtistIfNotExistsAsync(IDataContext dataContext, SpotifyApi.NetCore.SearchApi search, string name)
        {
            var bandData = await search.Search(query: name, "artist");
            var spotifyArtist = bandData.Artists.Items.FirstOrDefault();
            if (spotifyArtist == null)
            {
                return;
            }

            var artistInDb = await dataContext.Artists.Query()
                .Include(a => a.Albums)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.ExternalId == spotifyArtist.Id);
            if (artistInDb is null)
            {
                artistInDb = Mapping.MapperHelper.MapToArtist(spotifyArtist);
                dataContext.Artists.Add(artistInDb);
            }

            await AddArtistGenresAsync(dataContext, artistInDb, spotifyArtist);
            await AddArtistAlbumsAsync(dataContext, search, artistInDb);
        }

        private async Task AddArtistGenresAsync(IDataContext dataContext, Artist artistInDb, SpotifyArtist spotifyArtist)
        {
            foreach(var genre in spotifyArtist.Genres)
            {
                if(!artistInDb.Genres.Any(g => g.Genre.Name == genre))
                {
                    var genreInDb = dataContext.Genres.Query().FirstOrDefault(g => g.Name == genre);
                    if(genreInDb is null)
                    {
                        genreInDb = Mapping.MapperHelper.MapToGenre(genre);
                        dataContext.Genres.Add(genreInDb);
                    }

                    artistInDb.Genres.Add(new ArtistGenre { Artist = artistInDb, Genre = genreInDb });
                }
            }
        }

        private async Task AddArtistAlbumsAsync(
            IDataContext dataContext,
            SearchApi search, 
            Artist artist)
        {
            var albumsSearchResult = await search.Search($"artist:\'{artist.Name}\'", "album");
            if(albumsSearchResult.Albums.Total <= artist.Albums.Count())
            {
                return;
            }

            foreach(var spotifyAlbum in albumsSearchResult.Albums.Items)
            {
                await AddArtistAlbumAsync(dataContext, search, artist, spotifyAlbum);
            }
        }

        private async Task AddArtistAlbumAsync(
            IDataContext dataContext,
            SearchApi search,
            Artist artist,
            SpotifyAlbum spotifyAlbum)
        {
            var albumInDb = await dataContext.Albums.Query()
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(a => a.ExternalId == spotifyAlbum.Id);
            if(albumInDb == null)
            {
                albumInDb = Mapping.MapperHelper.MapToAlbum(spotifyAlbum);
                dataContext.Albums.Add(albumInDb);
            }
            albumInDb.Artist = artist;

            if(albumInDb.Tracks.Any())
            {
                return;
            }

            await AddAlbumTracksAsync(dataContext, search, albumInDb);
        }

        private async Task AddAlbumTracksAsync(
            IDataContext dataContext,
            SearchApi search,
            Album albumInDb)
        {
            var albumTracksApiResult = await search.Search($"album:{albumInDb.Title}", "track");
            if(albumInDb.Tracks.Count() >= albumTracksApiResult.Tracks.Total)
            {
                return;
            }

            foreach (var spotifyTrack in albumTracksApiResult.Tracks.Items)
            {
                await AddAlbumTrackAsync(dataContext, albumInDb, spotifyTrack);
            }
        }

        private async Task AddAlbumTrackAsync(IDataContext dataContext,
            Album albumInDb,
            SpotifyTrack spotifyTrack)
        {
            if(albumInDb.Tracks.Any(t => t.Title == spotifyTrack.Name))
            {
                return;
            }

            var trackInDb = Mapping.MapperHelper.MapToTrack(spotifyTrack);
            albumInDb.Tracks.Add(trackInDb);
            trackInDb.Album = albumInDb;
            dataContext.Tracks.Add(trackInDb);
        }

        private class SpotifyConfig : IConfiguration
        {
            public string this[string key]
            {
                get
                {
                    switch (key)
                    {
                        case nameof(SpotifyApiClientId): return SpotifyApiClientId;
                        case nameof(SpotifyApiClientSecret): return SpotifyApiClientSecret;
                        default: return null;
                    }
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
            public string SpotifyApiClientId => "759318169b66436ba800082fc88e0efb";
            public string SpotifyApiClientSecret => "92349ad8387845caa08b0ccef265e77c";

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }
        }
    }
}
