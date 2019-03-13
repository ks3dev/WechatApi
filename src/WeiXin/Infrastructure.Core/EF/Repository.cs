using Infrastructure.Core.DDD;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Core.EF
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        protected DbContext _dbContext;
        public Repository(IUnitOfWork dbContext)
        {
            _dbContext = dbContext as DbContext;
        }
        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public TEntity FindByID(int id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(m => m.Id == id);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
