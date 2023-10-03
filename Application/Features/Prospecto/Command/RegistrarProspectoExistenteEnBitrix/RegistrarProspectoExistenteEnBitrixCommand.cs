namespace Application.Features.Prospecto.Command.RegistrarProspectoExistenteEnBitrix
{
    using Domain.Enum;
    using MediatR;
    public sealed record RegistrarProspectoExistenteEnBitrixCommand(
        int ProspectoId
        ) : IRequest<int>;

}