using GraphQLInDotNet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    class DataSet<TEntity> : IDataSet<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DomainDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public DataSet(DomainDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            var entityEntry = this.dbSet.Add(entity);
            return entityEntry.Entity.Id;
        }

        public bool Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            return true;
        }

        public TEntity Get(int id)
        {
            return this.dbSet.Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Query()
        {
            return this.dbSet;
        }

        public bool Update(TEntity entity)
        {
            this.dbSet.Update(entity);
            return true;
        }
    }
}
