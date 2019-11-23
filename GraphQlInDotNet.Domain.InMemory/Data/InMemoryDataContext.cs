using GraphQlInDotNet.Data.InMemory.Data;
using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Domain.InMemory.Data
{
    public class InMemoryDataContext : IDataContext
    {
        private readonly IDictionary<Type, object> dataSetsDictionary;

        public IDataSet<Category> Categories { get; } = new CategoriesDataSet();

        public IDataSet<Artist> Artists => new InMemoryDataSet<Artist>();

        public IDataSet<Album> Albums => new InMemoryDataSet<Album>();

        public IDataSet<Track> Tracks => new InMemoryDataSet<Track>();

        public IDataSet<Genre> Genres => new InMemoryDataSet<Genre>();

        public InMemoryDataContext()
        {
            dataSetsDictionary = new Dictionary<Type, object>{
                { typeof(Category), Categories},
                { typeof(Genre), Genres},
                { typeof(Track), Tracks},
                { typeof(Album), Albums},
                { typeof(Artist), Albums},
            };
        }

        public IDataSet<TEntity> Set<TEntity>() where TEntity : IEntity
        {
            var type = typeof(TEntity);
            if (dataSetsDictionary.ContainsKey(type))
            {
                return (IDataSet<TEntity>)dataSetsDictionary[type];
            }

            return default;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
