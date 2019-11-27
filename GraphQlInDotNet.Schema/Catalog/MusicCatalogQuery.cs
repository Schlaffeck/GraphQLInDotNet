using GraphQlInDotNet.Schema.Catalog.Types;
using GraphQLInDotNet.Data;
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
            return this.dataContext.Artists.Query();
        }

        public IQueryable<AlbumDto> Albums()
        {
            return this.dataContext.Albums.QueryWithIncludes().Select(AlbumDto.Map).AsQueryable();
        }

        public IQueryable<Track> Tracks()
        {
            return this.dataContext.Tracks.QueryNoTracking();
        }

        public IQueryable<Genre> Genre()
        {
            return this.dataContext.Genres.QueryNoTracking();
        }
    }
}
