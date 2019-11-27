using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class AlbumDto
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public Uri UrlLink { get; set; }

        [GraphQLNonNullType]
        public string Title { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public int ArtistId { get; set; }

        public async Task<Artist> Artist([Service] IDataContext dataContext)
        {
            return await dataContext.Artists.GetAsync(this.ArtistId);
        }

        public IQueryable<Track> Tracks([Service] IDataContext dataContext)
        {
            return dataContext.Tracks.QueryNoTracking()
                .Where(t => t.AlbumId == Id);
        }

        internal static AlbumDto Map(Album album)
        {
            return new AlbumDto
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                ExternalId = album.ExternalId,
                ReleaseDate = album.ReleaseDate,
                UrlLink = new Uri(album.UrlLink),
                Title = album.Title
            };
        }
    }
}
