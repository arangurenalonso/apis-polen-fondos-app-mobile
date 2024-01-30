namespace Application.Features.Prospecto.Command.EliminarDeal
{
    using Application.Contracts.Repositories.Base;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Application.Contracts.ApiExterna;
    using Application.Contracts.Repositories;
    using System.Text.Json;

    public class RegistrarPorIdDealCommandHandler : IRequestHandler<EliminarDealCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBitrix24ApiService _bitrix24ApiService;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMaestroProspectoRepository _maestroProspectoRepository;
        private readonly IProspectoRepository _prospectoRepository;
        public RegistrarPorIdDealCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBitrix24ApiService bitrix24ApiService,
            IVendedorRepository vendedorRepository,
            IMaestroProspectoRepository maestroProspectoRepository,
            IProspectoRepository prospectoRepository
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bitrix24ApiService = bitrix24ApiService;
            _vendedorRepository = vendedorRepository;
            _maestroProspectoRepository = maestroProspectoRepository;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<bool> Handle(EliminarDealCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var dealEliminado = await _bitrix24ApiService.CRMDealDelete(request.Id);
                return dealEliminado;

            }
            catch (System.Exception e)
            {
                var mensaje = $"Error ocurrido al eliminar deal --- {e.Message}";
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                return false;
            }

        }

    }
}
