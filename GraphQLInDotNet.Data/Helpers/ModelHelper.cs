using GraphQLInDotNet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLInDotNet.Data.Helpers
{
    public static class ModelHelper
    {
        public static Artist AssignGenresToArtist(this IDataContext dataContext, Artist artist, IEnumerable<string> genresNames)
        {
            foreach(var genreName in genresNames)
            {
                AssignGenreToArtist(dataContext, artist, genreName);
            }
            return artist;
        }

        public static Artist AssignGenreToArtist(this IDataContext dataContext, Artist artist, string genreName)
        {
            if(artist.Genres.Any(g => g.Genre.Name == genreName))
            {
                return artist;
            }

            var genreInDb = dataContext.Genres.Query().FirstOrDefault(g => g.Name == genreName);
            if(genreInDb is null)
            {
                genreInDb = new Genre
                {
                    Name = genreName
                };
                dataContext.Genres.Add(genreInDb);
            }

            artist.Genres.Add(new ArtistGenre
            {
                Artist = artist,
                Genre = genreInDb
            });

            return artist;
        }
    }
}
