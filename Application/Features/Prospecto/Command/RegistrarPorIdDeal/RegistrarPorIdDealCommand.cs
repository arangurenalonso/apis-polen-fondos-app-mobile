namespace Application.Features.Prospecto.Command.RegistrarPorIdDeal
{
    using MediatR;
    public sealed record RegistrarPorIdDealCommand(
        int Id
        ) : IRequest<int>;
}
