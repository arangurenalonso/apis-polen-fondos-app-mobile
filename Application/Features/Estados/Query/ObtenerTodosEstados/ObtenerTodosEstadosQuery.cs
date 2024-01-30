namespace Application.Features.Estados.Query.ObtenerTodosEstados
{
    using Application.Models.DTOResponse;
    using MediatR;

    public sealed record ObtenerTodosEstadosQuery(
        ) : IRequest<List<SearchViewModel>>;
}
