using GraphQlInDotNet.Schema.Catalog.Types.Filter;
using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Helpers;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using HotChocolate.Types;
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

        // we do not want to reveal reference back to artist
        //public async Task<Artist> Artist([Service] IDataContext dataContext)
        //{
        //    return await dataContext.Artists.GetAsync(this.ArtistId);
        //}

        [UseSorting]
        [UseFiltering(FilterType = typeof(TrackFilterType))]
        public IQueryable<Track> Tracks([Service] IDataContext dataContext, int? skip = 0, int? take = 0)
        {
            return dataContext.Tracks.QueryNoTracking()
                .Where(t => t.AlbumId == Id).AddSkipTake(skip, take);
        }

        internal static AlbumDto Map(Album album)
        {
            return new AlbumDto
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                ExternalId = album.ExternalId,
                ReleaseDate = album.ReleaseDate,
                UrlLink = album.UrlLink != null ? new Uri(album.UrlLink) : null,
                Title = album.Title
            };
        }
    }
}
