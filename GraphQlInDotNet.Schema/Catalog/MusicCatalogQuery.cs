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
            return this.dataContext.Artists.QueryWithIncludes();
        }
    }
}
