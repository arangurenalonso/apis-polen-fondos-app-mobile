namespace Application.Features.Prospecto.Query.ObtenerDiscrepanciaDeOrigenes
{
    using MediatR;
    public sealed record ObtenerDiscrepanciaDeOrigenesQuery(
        ) : IRequest<int>;
}
