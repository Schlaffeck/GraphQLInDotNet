using GraphQLInDotNet.Data.Models;
using System.Threading.Tasks;

namespace GraphQLInDotNet.Data
{
    public interface IDataContext
    {
        IDataSet<TEntity> Set<TEntity>() where TEntity : IEntity;


        IDataSet<Artist> Artists{ get; }

        IDataSet<Album> Albums { get; }

        IDataSet<Track> Tracks { get; }

        IDataSet<Genre> Genres { get; }

        Task SaveChangesAsync();
    }
}
