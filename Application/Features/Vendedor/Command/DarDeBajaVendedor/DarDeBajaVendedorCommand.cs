namespace Application.Features.Vendedor.Command.DarDeBajaVendedor
{
    using MediatR;
    public sealed record DarDeBajaVendedorCommand(
        string CodVendedor 
        ) : IRequest<string>; 
}
