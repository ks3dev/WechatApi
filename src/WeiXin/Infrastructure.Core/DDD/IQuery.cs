using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Core.DDD
{
    public interface IQuery<TEntity> where TEntity : IEntity
    {
        TEntity GetFirst(Expression<Func<TEntity, bool>> condition);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> condition);
    }
}
