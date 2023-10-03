namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
{
    using MediatR;
    public sealed record RegistrarProspectoRedesSocialesCommand(
        string Anuncio,
        string Plataforma,
        string Nombre,
        string? Apellido,
        string Telefono,
        string? Email
        ) : IRequest<int>;
}
