namespace Application.Features.Reportes.ObtenerReporte1
{
    using Application.Models.DTOResponse;
    using MediatR;

    public sealed record ObtenerReporte1Query(
        DateTime? start_at,
        DateTime? end_at,
        string? ven_gercod,
        string? ven_gescod,
        string? ven_supcod,
        string? ven_cod,
        string? prioridad,
        string? states,
        string? medio
    ) : IRequest<List<Reporte1DTOResponse>>;
}
