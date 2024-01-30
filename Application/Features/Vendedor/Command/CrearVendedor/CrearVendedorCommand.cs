namespace Application.Features.Vendedor.Command.CrearVendedor
{
    using MediatR;
    public sealed record CrearVendedorCommand(
        string CodVendedor,
        string CodSupervisor,
        string Zona

        ) : IRequest<string>;
}
