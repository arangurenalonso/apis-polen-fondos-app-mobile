namespace Application.Features.Vendedor.Query.ObtenerGerentes
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Models.DTOResponse;

    public class ObtenerGerentesQueryHandler : IRequestHandler<ObtenerGerentesQuery, List<SearchViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVendedorRepository _vendedorRepository;

        public ObtenerGerentesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IVendedorRepository vendedorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vendedorRepository = vendedorRepository;
        }


        public async Task<List<SearchViewModel>> Handle(ObtenerGerentesQuery request, CancellationToken cancellationToken)
        {
            var vendedores = await _vendedorRepository.ObtenerVendedoresPorJerarquiaComercial(request.Cargo,
                request.CodGerente,
                request.CodGestor,
                request.CodSupervisor,
                request.CodVendedor
                );

            return _mapper.Map<List<SearchViewModel>>(vendedores);
        }
    }
}
