namespace Application.Features.Prospecto.Command.EliminarDeal
{
    using MediatR;
    public sealed record EliminarDealCommand(
        int Id
        ) : IRequest<bool>;
}
