namespace Application.Features.Prospecto.Query.ObtenerDiscrepanciaDeOrigenes
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Mappings.Prospecto.DTO;
    using Application.Features.Prospecto.Query.ObtenerDatosProspectoPorVendedor;
    using Application.Contracts.ApiExterna;

    public class ObtenerDiscrepanciaDeOrigenesQueryHandler : IRequestHandler<ObtenerDiscrepanciaDeOrigenesQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBitrix24ApiService _bitrix24ApiService;

        public ObtenerDiscrepanciaDeOrigenesQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IBitrix24ApiService bitrix24ApiService )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bitrix24ApiService = bitrix24ApiService;
        }


        public async Task<int> Handle(ObtenerDiscrepanciaDeOrigenesQuery request, CancellationToken cancellationToken)
        {
            var contactos = await _bitrix24ApiService.CRMContactList();
            return 1;
        }
    }
}
