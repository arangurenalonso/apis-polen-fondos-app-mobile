
namespace Application.Features.Prospecto.Command.RegistrarProspectoFacebook
{
    using MediatR;
    public sealed record RegistrarProspectoFacebookCommand(
        DateTime Fecha,
        string? Anuncio,
        string? Plataforma,
        string? Nombre,
        string? Apellido,
        string? Telefono,
        string? Email
        ) : IRequest<int>;
}
