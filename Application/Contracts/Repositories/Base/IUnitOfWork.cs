namespace Application.Contracts.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
