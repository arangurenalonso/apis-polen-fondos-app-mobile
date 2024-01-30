namespace Application.Features.Medios.Query.ObtenerTodosEstados
{
    using Application.Models.DTOResponse;
    using MediatR;

    public sealed record ObtenerTodosMediosQuery(
        ) : IRequest<List<SearchViewModel>>;
}
