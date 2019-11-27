using System.Linq;
using System.Threading.Tasks;

namespace GraphQLInDotNet.Data
{
    public interface IDataSet<TEntity> where TEntity : IEntity
    {
        TEntity Get(int id);

        Task<TEntity> GetAsync(int id);

        int Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> QueryNoTracking();

        IQueryable<TEntity> QueryWithIncludes();
    }
}
