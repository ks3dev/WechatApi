namespace Infrastructure.Core.DDD
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
