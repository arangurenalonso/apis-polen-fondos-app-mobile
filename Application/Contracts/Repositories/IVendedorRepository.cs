namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    public interface IVendedorRepository : IRepositoryBase<Vendedores>
    {
        Task<Vendedores> ObtenerVendedorAsignado(int idZona = 0, string? codSupervisor = "");
        Task<Vendedores> ObtenerVendedorPorCodigo(string codVendedor);
        Task<(string nombreDirector, string nombreGerenteZona)> ObtenerJerarQuiaComercial(Vendedores vendedor);
        Task<List<Vendedores>> ObtenerVendedoresPorJerarquiaComercial(
            string cargo,
            string? codGerente = null,
            string? codGestor = null,
            string? codSupervisor = null,
            string? codVendedor = null
            );
    } 
}
