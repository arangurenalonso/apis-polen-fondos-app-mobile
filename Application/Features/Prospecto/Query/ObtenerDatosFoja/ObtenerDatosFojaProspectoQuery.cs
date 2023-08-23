namespace Application.Features.Prospecto.Query.ObtenerDatosFoja
{
    using Application.Mappings.Prospecto.DTO;
    using MediatR;
    public sealed record ObtenerDatosFojaProspectoQuery(
        int ProspectoId
        ) : IRequest<SpGetListaDatosFojaResult>;

}
