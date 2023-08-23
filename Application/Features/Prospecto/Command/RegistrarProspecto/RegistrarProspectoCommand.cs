namespace Application.Features.Prospecto.Command.RegistrarProspecto
{
    using Domain.Enum;
    using MediatR;
    public sealed record RegistrarProspectoCommand(
        int MaestroProspectoId,
        bool? Interes,
        int linea, 
        string OrigenVenta,
        int Medio,
        string? Comentario,
        string? CodVendedor,
        string? CodSupervisor,
        string? CodGestor,
        string? CodGerente,
        int Zona
        ) : IRequest<int>;

}