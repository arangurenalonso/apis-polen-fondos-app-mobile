namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Application.Mappings.Prospecto.DTO;
    using Domain.Entities;
    public interface IVendedorRepository : IRepositoryBase<Vendedores>
    {
        Task<Vendedores> ObtenerVendedorAsignado(int idZona = 0, string? codSupervisor = "");
        Task<Vendedores> ObtenerVendedorPorCodigo(string codVendedor);
    } 
}
