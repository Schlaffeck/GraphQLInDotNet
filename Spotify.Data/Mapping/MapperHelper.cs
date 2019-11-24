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
                ReleaseDate = ParseDateTime(spotifyAlbum.ReleaseDate, spotifyAlbum.ReleaseDatePrecision),
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

        public static DateTimeOffset ParseDateTime(string input, string spotifyPrecision)
        {
            if(spotifyPrecision == "day")
            {
                return DateTimeOffset.ParseExact(input, "yyyy-MM-dd", null);
            }

            if (spotifyPrecision == "month")
            {
                return DateTimeOffset.ParseExact(input+"-01", "yyyy-MM-dd", null);
            }

            if (spotifyPrecision == "year")
            {
                return new DateTimeOffset(int.Parse(input), 1, 1, 0, 0, 0, TimeSpan.Zero);
            }

            return DateTimeOffset.MinValue;
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
