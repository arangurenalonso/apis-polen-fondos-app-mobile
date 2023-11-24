namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
{
    public sealed record RegistrarProspectoRedesSocialesCommandDTORequest(
        string Anuncio,
        string Plataforma,
        string Nombre,
        string? Apellido,
        string Telefono,
        string? Email
        ) ;
}
