namespace Application.Features.Prospecto.Command.RegistrarMaestroProspecto
{
    using Domain.Enum;
    using MediatR;
    public sealed record RegistrarMaestroProspectoCommand(
    TipoDocumentoEnum? TipoDoc,
    string? NumDoc,
    string? ApePaterno,
    string? ApeMaterno,
    string Nombres,
    string? RazonSocial,
    string? Direccion,
    GeneroEnum? Genero,
    DateTime? Fnacimiento,
    string? DepId,
    string? ProId,
    string? DisId,
    string Cel1,
    string? Cel2,
    string? Telfijo,
    string? Email
        ) : IRequest<int>;

}
