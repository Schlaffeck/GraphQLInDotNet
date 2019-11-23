using GraphQLInDotNet.Data.Models;

namespace GraphQLInDotNet.Data
{
    public interface IDataContext
    {
        IDataSet<TEntity> Set<TEntity>() where TEntity : IEntity;

        IDataSet<Category> Categories { get; }

        IDataSet<Artist> Artists{ get; }

        IDataSet<Album> Albums { get; }

        IDataSet<Track> Tracks { get; }

        IDataSet<Genre> Genres { get; }
    }
}
