using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlInDotNet.GraphQl
{
    public class Query
    {
        private readonly IDataContext dataContext;

        public Query(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Artist> Artists(int? skip = 0, int? take = 10)
        {
            var quer = this.dataContext.Artists.QueryWithIncludes();

            return AddSkipTake(quer, skip, take);
        }

        private IQueryable<T> AddSkipTake<T>(IQueryable<T> query, int? skip = 0, int? take = 20)
        {
            if (skip.HasValue && skip.Value >= 0)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue && take.Value >= 0)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}
