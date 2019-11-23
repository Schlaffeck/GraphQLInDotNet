using GraphQLInDotNet.Data.Models;
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
