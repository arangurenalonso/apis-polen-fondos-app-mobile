namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Application.Mappings.Prospecto.DTO;
    using Domain.Entities;
    public interface IProspectoRepository: IRepositoryBase<Prospectos>
    {
        Task<SPGetDatosProspectoVendResponse> ObtenerDatosProspectoPorVendedor(string CodigoVendedor, string nroDocumento);
        Task<SpGetListaDatosFojaResult> ObtenerDatosFoja(int proid);
    }
}
