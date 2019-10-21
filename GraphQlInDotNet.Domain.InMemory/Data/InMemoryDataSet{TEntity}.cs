using GraphQLInDotNet.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Domain.InMemory.Data
{
    class InMemoryDataSet<TEntity> : IDataSet<TEntity>
        where TEntity : IEntity
    {
        private Dictionary<int, TEntity> entities;
        private int lastKey = 0;

        public InMemoryDataSet()
        {
            entities = new Dictionary<int, TEntity>();
        }

        public int Add(TEntity entity)
        {
            if(entities.ContainsKey(entity.Id))
            {
                entities[entity.Id] = entity;
                return entity.Id;
            }

            var newKey = Interlocked.Increment(ref lastKey);
            entity.Id = newKey;
            entities.Add(newKey, entity);
            return newKey;
        }

        public bool Delete(TEntity entity)
        {
            return entities.Remove(entity.Id);
        }

        public TEntity Get(int id)
        {
            return entities.ContainsKey(id) ?
                this.entities[id] : default;
        }

        public Task<TEntity> GetAsync(int id)
        {
            return Task.FromResult(Get(id));
        }

        public IQueryable<TEntity> Query()
        {
            return entities.Values.AsQueryable();
        }

        public bool Update(TEntity entity)
        {
            if(!entities.ContainsKey(entity.Id))
            {
                return false;
            }

            entities[entity.Id] = entity;

            return true;
        }
    }
}
