using GraphQLInDotNet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    internal class DataSet<TEntity> : IDataSet<TEntity>
        where TEntity : class, IEntity
    {
        protected DomainDbContext Context { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }

        public DataSet(DomainDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual int Add(TEntity entity)
        {
            var entityEntry = this.DbSet.Add(entity);
            return entityEntry.Entity.Id;
        }

        public virtual bool Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            return true;
        }

        public virtual TEntity Get(int id)
        {
            return this.QueryWithIncludes().FirstOrDefault(e => e.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await this.QueryWithIncludes().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return this.DbSet;
        }

        public virtual IQueryable<TEntity> QueryWithIncludes()
        {
            return Query();
        }

        public virtual bool Update(TEntity entity)
        {
            this.DbSet.Update(entity);
            return true;
        }

        public virtual IQueryable<TEntity> QueryNoTracking()
        {
            return this.DbSet.AsNoTracking();
        }
    }
}
