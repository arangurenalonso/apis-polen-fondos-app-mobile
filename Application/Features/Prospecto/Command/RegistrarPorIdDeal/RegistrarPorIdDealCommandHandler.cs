using Application.Contracts.Repositories.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Contracts.ApiExterna;
using Application.Contracts.Repositories;
using Application.Models.ConsumoApi.Bitrix24.Entities;
using Application.Models.ConsumoApi.Bitrix24.Models;
using System.Text.Json;
using Application.Exception;

namespace Application.Features.Prospecto.Command.RegistrarPorIdDeal
{
    public class RegistrarPorIdDealCommandHandler : IRequestHandler<RegistrarPorIdDealCommand, int>
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


        public async Task<int> Handle(RegistrarPorIdDealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var zonaId = 0;//Si ZonaId = 0 => Enviar a todos
                var deal = await _bitrix24ApiService.CRMDealGet(request.Id.ToString());
                var origenVenta = (await _unitOfWork.Repository<OrigenVentas>().GetAsync(x => x.CoridatId == deal.SOURCE_ID)).FirstOrDefault();
                if (origenVenta == null)
                {
                    throw new NotFoundException($"No se encontro el origden de ventas con deal.SOURCE_ID = '{deal.SOURCE_ID}'");
                }
                var contact = await _bitrix24ApiService.CRMContactGet(deal.CONTACT_ID);

                var existeMaestroProspecto = await _maestroProspectoRepository.VerificarRegistroPrevioMaestroProspecto(contact.PHONE[0].VALUE);
                int idMaestroProspecto = 0;
                bool ingresarProspecto = false;
                if (existeMaestroProspecto == null)
                {
                    idMaestroProspecto = await _maestroProspectoRepository.EstablerDatosMinimoYRegistrarMaestroProspecto(_mapper.Map<MaestroProspecto>(contact), contact.ID);
                    ingresarProspecto = true;
                }
                else
                {
                    idMaestroProspecto = existeMaestroProspecto.MaeId;
                    ingresarProspecto = await _prospectoRepository.VerificarIngresoProspecto(existeMaestroProspecto.MaeId);
                }
                var idProspecto = 0;
                if (ingresarProspecto)
                {
                    var vendedorAsignado = await _vendedorRepository.ObtenerVendedorAsignado(zonaId);
                    var prospectoToCreate = _mapper.Map<Prospectos>(deal);
                    prospectoToCreate.MedId = 10;
                    idProspecto = await _prospectoRepository.EstablecerDatosMinimosYRegistrarProspecto(prospectoToCreate, idMaestroProspecto, deal.ID.ToString(), vendedorAsignado, vendedorAsignado.ZonId, origenVenta.Corivta);
                    await _bitrix24ApiService.ActualizarDealBitrix24(deal, vendedorAsignado.BitrixID);
                    await _bitrix24ApiService.ActualizarContactoBitrix24(contact, vendedorAsignado.BitrixID);

                }
                else
                {

                    throw new ApplicationException($"Lead Repetido");
                }

                return idProspecto;
            }
            catch (System.Exception e)
            {
                var mensaje = $"Error ocurrido en RegistrarPorIdDeal --- {e.Message}";
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                await Task.Delay(TimeSpan.FromSeconds(120));
                return 0;
            }

        }
       
    }

}
