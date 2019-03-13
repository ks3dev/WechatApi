using Infrastructure.Core.DDD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Core.EF
{
    public class Query<TEntity> : IQuery<TEntity>
        where TEntity : class, IEntity
    {
        protected DbContext _dbContext;
        public Query(IUnitOfWork dbContext)
        {
            _dbContext = dbContext as DbContext;
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
        {
            return _dbContext.Set<TEntity>().Where(condition).ToList();
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> condition)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(condition);
        }
    }
}
