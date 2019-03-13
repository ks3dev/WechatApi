namespace Infrastructure.Core.DDD
{
    public interface IRepository<TEntity> 
        where TEntity : IAggregateRoot
    {
        TEntity FindByID(int id);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
    }
}
