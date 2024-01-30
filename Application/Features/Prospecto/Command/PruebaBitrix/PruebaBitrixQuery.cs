namespace Application.Features.Prospecto.Command.PruebaBitrix
{
    using MediatR;
    public sealed record PruebaBitrixQuery(
        int Id
        ) : IRequest<bool>;
}
