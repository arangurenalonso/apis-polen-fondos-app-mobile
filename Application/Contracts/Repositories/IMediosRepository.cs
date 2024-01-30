namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    public interface IMediosRepository : IRepositoryBase<Medios>
    {
        Task<List<Medios>> ObtenerTodos();
    }
}
