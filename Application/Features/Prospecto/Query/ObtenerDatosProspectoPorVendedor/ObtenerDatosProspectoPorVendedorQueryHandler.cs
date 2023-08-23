namespace Application.Features.Prospecto.Query.ObtenerDatosProspectoPorVendedor
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Mappings.Prospecto.DTO;

    public class ObtenerDatosProspectoPorVendedorQueryHandler : IRequestHandler<ObtenerDatosProspectoPorVendedorQuery, SPGetDatosProspectoVendResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IProspectoRepository _prospectoRepository { get; }
        public ObtenerDatosProspectoPorVendedorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IProspectoRepository prospectoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<SPGetDatosProspectoVendResponse> Handle(ObtenerDatosProspectoPorVendedorQuery request, CancellationToken cancellationToken)
        {
            var result = await _prospectoRepository.ObtenerDatosProspectoPorVendedor(request.CodVendedor, request.nroDocumento);
            if (result == null)
            {
                throw new ApplicationException($"No se encontro el registro con el codigo vendedor:{request.CodVendedor} y número de documento: {request.nroDocumento}");
            }

            return result;
        }
    }
}
