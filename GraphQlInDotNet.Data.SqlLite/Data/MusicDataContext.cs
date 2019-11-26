using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    public class MusicDataContext : IDataContext
    {
        private readonly IDictionary<Type, object> dataSetsDictionary;
        private readonly DomainDbContext domainDbContext;

        public IDataSet<Artist> Artists { get; }

        public IDataSet<Album> Albums { get; }

        public IDataSet<Track> Tracks { get; }

        public IDataSet<Genre> Genres { get; }

        IDataSet<TEntity> IDataContext.Set<TEntity>()
        {
            var type = typeof(TEntity);
            if (dataSetsDictionary.ContainsKey(type))
            {
                return (IDataSet<TEntity>)dataSetsDictionary[type];
            }

            return default;
        }

        public async Task SaveChangesAsync()
        {
            await this.domainDbContext.SaveChangesAsync();
        }

        public MusicDataContext(DomainDbContext domainDbContext)
        {
            dataSetsDictionary = new Dictionary<Type, object>{
                { typeof(Genre), Genres},
                { typeof(Track), Tracks},
                { typeof(Album), Albums},
                { typeof(Artist), Albums},
            };
            this.domainDbContext = domainDbContext;
            this.Albums = new DataSet<Album>(this.domainDbContext);
            this.Genres = new DataSet<Genre>(this.domainDbContext);
            this.Artists = new ArtistsDataSet(this.domainDbContext);
            this.Tracks = new DataSet<Track>(this.domainDbContext);
        }
    }
}
