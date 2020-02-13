using GraphQlInDotNet.Schema.Catalog.Types;
using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Helpers;
using GraphQLInDotNet.Data.Models;
using System.Linq;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class MusicCatalogQuery
    {
        private readonly IDataContext dataContext;

        public MusicCatalogQuery(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Artist> Artists()
        {
            var query = this.dataContext.Artists.QueryNoTracking();

            return query;
        }

        public IQueryable<AlbumDto> Albums()
        {
            return this.dataContext.Albums.QueryWithIncludes().Select(AlbumDto.Map).AsQueryable();
        }

        public IQueryable<Track> Tracks(int? skip = 0, int? take = 20)
        {
            var query = this.dataContext.Tracks.QueryNoTracking();

            return query.AddSkipTake(skip, take);
        }

        public IQueryable<Genre> Genres(
            int? forArtist =null,
            int? skip = 0, int? take = 20, string nameLike = null, GenreOrderBy orderBy = null)
        {
            var query = this.dataContext.Genres.QueryNoTracking();

            if(forArtist.HasValue)
            {
                query = query.Where(g => g.Artists.Any(a => a.ArtistId == forArtist));
            }

            if(!string.IsNullOrWhiteSpace(nameLike))
            {
                query = query.Where(g => g.Name.Contains(nameLike));
            }

            if(orderBy != null)
            {
                if(orderBy.Descending)
                {
                    query = orderBy.FieldName == GenreOrderBy.FieldNameEnum.Name
                        ? query.OrderByDescending(g => g.Name)
                        : query.OrderByDescending(g => g.Id);
                }
                else
                {
                    query = orderBy.FieldName == GenreOrderBy.FieldNameEnum.Name
                        ? query.OrderBy(g => g.Name)
                        : query.OrderBy(g => g.Id);
                }
            }

            return query.AddSkipTake(skip, take);
        }

        public class GenreOrderBy
        {
            public enum FieldNameEnum
            {
                Name,
                Id
            }

            public FieldNameEnum FieldName { get; set; }

            public bool Descending { get; set; }
        }

    }
}
