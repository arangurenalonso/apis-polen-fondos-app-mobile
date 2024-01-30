namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
{
    using MediatR;
    public sealed record RegistrarProspectoRedesSocialesCommand(
        string Anuncio,
        string Plataforma,
        string Nombre,
        string? Apellido,
        string Telefono,
        string? Email,
        bool EsMasivo,
        bool EsUltimoRegistro,
        int NumeroRegistro,
        bool VaBitrix=true
        ) : IRequest<int>;
}
