namespace Application.Features.Vendedor.Query.ObtenerGerentes
{
    using Application.Models.DTOResponse;
    using MediatR;

    public sealed record ObtenerGerentesQuery(
            string Cargo,
            string? CodGerente = null,
            string? CodGestor = null,
            string? CodSupervisor = null,
            string? CodVendedor = null
        ) : IRequest<List<SearchViewModel>>;
}
