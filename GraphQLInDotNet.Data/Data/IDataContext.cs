using GraphQLInDotNet.Data.Models;

namespace GraphQLInDotNet.Domain.Data
{
    public interface IDataContext
    {
        IDataSet<TEntity> Set<TEntity>() where TEntity : IEntity;

        IDataSet<Category> Categories { get; }
    }
}
