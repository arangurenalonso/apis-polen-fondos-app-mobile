namespace Application.Features.Vendedor.Command.CrearVendedor
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Contracts.ApiExterna;

    public class CrearVendedorCommandHandler : IRequestHandler<CrearVendedorCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IBitrix24ApiService _bitrix24ApiService;

        public CrearVendedorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IVendedorRepository vendedorRepository,
            IBitrix24ApiService bitrix24ApiService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendedorRepository = vendedorRepository;
            _bitrix24ApiService = bitrix24ApiService;
        }


        public async Task<string> Handle(CrearVendedorCommand request, CancellationToken cancellationToken)
        {
            var codVendedor=request.CodVendedor;
            var ss=request.Zona;
            return "Hola MUndo";
        }
    }

}
