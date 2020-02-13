using System.Linq;

namespace GraphQLInDotNet.Data.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> AddSkipTake<T>(this IQueryable<T> query, int? skip = 0, int? take = 20)
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
