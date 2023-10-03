namespace Application.Features.Reportes.Command.ExportarReporte1
{
    using Application.Models.DTOResponse;
    using MediatR;

    public sealed record ExportarReporte1Query(
        DateTime? start_at,
        DateTime? end_at,
        string? ven_gercod,
        string? ven_gescod,
        string? ven_supcod,
        string? ven_cod,
        string? prioridad,
        string? states,
        string? medio
    ) : IRequest<Stream>;
}
