namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Application.Mappings.Prospecto.DTO;
    using Domain.Entities;
    public interface IProspectoRepository: IRepositoryBase<Prospectos>
    { 
        Task<Prospectos> ObtenerProspectoPorId(int idProspecto);
        Task<SPGetDatosProspectoVendResponse> ObtenerDatosProspectoPorVendedor(string CodigoVendedor, string nroDocumento);
        Task<SpGetListaDatosFojaResult> ObtenerDatosFoja(int proid); 
        Task<int> EstablecerDatosMinimosYRegistrarProspecto(Prospectos prospecto,
            int idMaestroProspecto, string idDealBitrix24, Vendedores vendedor, int zonaId, string idOrigen, string? enumCampignOrigin
            , DateTime fechaGestion
            , DateTime fechaAsignacion
        );
        Task<(bool, Prospectos? prospecto)> VerificarIngresoProspecto(int idMaestroProspecto);
        Task<Prospectos?> ObtenerUltimoProspectoPorIdMaestroProspecto(int idMaestroProspecto);
        Task<OrigenVentas> ObtenerOrigenVentaProspecto(string? corivta);
        Task<PuntoDeVenta> ObtenerPuntoVentaProspecto(string? cptovta);
    }
}
