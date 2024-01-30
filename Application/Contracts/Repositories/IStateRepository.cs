namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    public interface IStateRepository : IRepositoryBase<StateEntity>
    {
        Task<List<StateEntity>> ObtenerTodos();
    }
}
