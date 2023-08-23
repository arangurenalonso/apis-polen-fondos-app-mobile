namespace Application.Features.Prospecto.Query.ObtenerDatosProspectoPorVendedor
{
    using Application.Mappings.Prospecto.DTO;
    using MediatR;
    public sealed record ObtenerDatosProspectoPorVendedorQuery(
        string CodVendedor,
        string nroDocumento
        ) : IRequest<SPGetDatosProspectoVendResponse>;

}
