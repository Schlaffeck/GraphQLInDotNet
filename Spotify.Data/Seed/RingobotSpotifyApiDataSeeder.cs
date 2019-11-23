using GraphQLInDotNet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            await AddArtists(dataContext);
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

        private async Task AddArtists(IDataContext dataContext)
        {
            var search = new SearchApi(this.httpClient, this.apiService);

            var bandsFile = Files.FIlesResource.bands;
            var artistNames = bandsFile.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var artistName in artistNames)
            {
                await AddArtistIfNotExistsAsync(dataContext, search, artistName);
            }
        }

        private async Task AddArtistIfNotExistsAsync(IDataContext dataContext, SearchApi search, string name)
        {
            if (dataContext.Artists.Query().Any(a => a.Name == name))
            {
                return;
            }

            var bandData = await search.Search(query: name, "artist");
            var spotifyArtist = bandData.Artists.Items.FirstOrDefault();
            if (spotifyArtist == null)
            {
                return;
            }
            if(dataContext.Artists.Query().Any(a => a.ExternalId == spotifyArtist.Id))
            {
                return;
            }

            var artist = Mapping.MapperHelper.MapToArtist(spotifyArtist);
            dataContext.Artists.Add(artist);
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
