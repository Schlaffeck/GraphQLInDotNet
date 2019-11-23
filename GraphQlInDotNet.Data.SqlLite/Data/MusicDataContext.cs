using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    public class MusicDataContext : IDataContext
    {
        private readonly IDictionary<Type, object> dataSetsDictionary;
        private readonly DomainDbContext domainDbContext;

        public IDataSet<Category> Categories => new DataSet<Category>(this.domainDbContext);

        public IDataSet<Artist> Artists => new DataSet<Artist>(this.domainDbContext);

        public IDataSet<Album> Albums => new DataSet<Album>(this.domainDbContext);

        public IDataSet<Track> Tracks => new DataSet<Track>(this.domainDbContext);

        public IDataSet<Genre> Genres => new DataSet<Genre>(this.domainDbContext);

        IDataSet<TEntity> IDataContext.Set<TEntity>()
        {
            var type = typeof(TEntity);
            if (dataSetsDictionary.ContainsKey(type))
            {
                return (IDataSet<TEntity>)dataSetsDictionary[type];
            }

            return default;
        }

        public MusicDataContext(DomainDbContext domainDbContext)
        {
            dataSetsDictionary = new Dictionary<Type, object>{
                { typeof(Category), Categories},
                { typeof(Genre), Genres},
                { typeof(Track), Tracks},
                { typeof(Album), Albums},
                { typeof(Artist), Albums},
            };
            this.domainDbContext = domainDbContext;
        }
    }
}
