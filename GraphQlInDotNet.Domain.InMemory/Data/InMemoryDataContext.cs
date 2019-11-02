using GraphQlInDotNet.Data.InMemory.Data;
using GraphQLInDotNet.Data.Models;
using GraphQLInDotNet.Domain.Data;
using System;
using System.Collections.Generic;

namespace GraphQlInDotNet.Domain.InMemory.Data
{
    public class InMemoryDataContext : IDataContext
    {
        private readonly IDictionary<Type, object> dataSetsDictionary;

        public IDataSet<Category> Categories { get; } = new CategoriesDataSet();

        public InMemoryDataContext()
        {
            dataSetsDictionary = new Dictionary<Type, object>{
                { typeof(Category), Categories}
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
    }
}
