namespace Application.Features.Prospecto.Query.ObtenerDatosFoja
{
    using AutoMapper;
    using MediatR;
    using Application.Contracts.Repositories.Base;
    using Application.Contracts.Repositories;
    using Application.Mappings.Prospecto.DTO;
    public class ObtenerDatosFojaProspectoQueryHandler : IRequestHandler<ObtenerDatosFojaProspectoQuery, SpGetListaDatosFojaResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IProspectoRepository _prospectoRepository { get; }
        public ObtenerDatosFojaProspectoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IProspectoRepository prospectoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<SpGetListaDatosFojaResult> Handle(ObtenerDatosFojaProspectoQuery request, CancellationToken cancellationToken)
        {
            var result = await _prospectoRepository.ObtenerDatosFoja(request.ProspectoId);
            if (result == null)
            {
                throw new ApplicationException($"No se encontro el prospecto con Codigo ID:{request.ProspectoId}");
            }

            return result;
        }
    }
}
