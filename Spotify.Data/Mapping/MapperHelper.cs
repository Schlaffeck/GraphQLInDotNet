using GraphQLInDotNet.Data.Models;
using System;
using System.Collections.Generic;

namespace Spotify.Data.Mapping
{
    public static class MapperHelper
    {
        public static Artist MapToArtist(SpotifyApi.NetCore.Artist spotifyArtist)
        {
            return new Artist
            {
                Name = spotifyArtist.Name,
                ExternalId = spotifyArtist.Id,
                UrlLink = spotifyArtist.Uri,
                Genres = new List<ArtistGenre>()
            };
        }

        public static Album MapToAlbum(SpotifyApi.NetCore.Album spotifyAlbum)
        {
            return new Album
            {
                Title = spotifyAlbum.Name,
                ExternalId = spotifyAlbum.Id,
                ReleaseDate = DateTimeOffset.ParseExact(spotifyAlbum.ReleaseDate, "yyyy-MM-dd", null),
                UrlLink = spotifyAlbum.Uri,
                Tracks = new List<Track>()
            };
        }

        public static Track MapToTrack(SpotifyApi.NetCore.Track spotifyTrack)
        {
            return new Track
            {
                Title = spotifyTrack.Name,
                Duration = TimeSpan.FromMilliseconds(spotifyTrack.DurationMs),
                ExternalId = spotifyTrack.Id,
                UrlLink = spotifyTrack.Uri,
            };
        }

        public static Genre MapToGenre(string name)
        {
            return new Genre
            {
                Name = name,
                Artists = new List<ArtistGenre>()
            };
        }
    }
}
