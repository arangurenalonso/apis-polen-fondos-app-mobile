﻿namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
{
    using Application.Contracts.Repositories.Base;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Application.Contracts.ApiExterna;
    using Domain.Enum.Dictionario;
    using Application.Contracts.Repositories;
    using System.Text.Json;
    using Domain.Enum;

    public class RegistrarProspectoRedesSocialesCommandHandler : IRequestHandler<RegistrarProspectoRedesSocialesCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBitrix24ApiService _bitrix24ApiService;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMaestroProspectoRepository _maestroProspectoRepository;
        private readonly IProspectoRepository _prospectoRepository;
        public RegistrarProspectoRedesSocialesCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IBitrix24ApiService bitrix24ApiService,
            IVendedorRepository vendedorRepository,
            IMaestroProspectoRepository maestroProspectoRepository,
            IProspectoRepository prospectoRepository )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bitrix24ApiService = bitrix24ApiService;
            _vendedorRepository = vendedorRepository;
            _maestroProspectoRepository = maestroProspectoRepository;
            _prospectoRepository = prospectoRepository;
        }


        public async Task<int> Handle(RegistrarProspectoRedesSocialesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoNegociacion =
                    EnumDictionaryProvider.TipoNegociacionEnumDict[TipoNegociacionEnum.VENTA_DIGITAL_B2C];

                var enumCampignOrigin = getOriginCapaña(request.Plataforma!, request.Anuncio!).ToString();
                var zonaId = getIdZonaByAnuncio(request.Anuncio);
                if (zonaId==3)
                {
                    zonaId = 2;
                }
                var existeMaestroProspecto = await _maestroProspectoRepository.VerificarRegistroPrevioMaestroProspecto(request.Telefono);
                var vendedorAsignado = await _vendedorRepository.ObtenerVendedorAsignado(zonaId);
                var (nombreDirector, nombreGerenteZona) = await _vendedorRepository.ObtenerJerarQuiaComercial(vendedorAsignado);
                string idContactBitrix24 = "";
                int idMaestroProspecto = 0;
                bool seDebeIngresarProspecto = false;
                Prospectos? prospectoIngresado = null;

                if (existeMaestroProspecto == null)
                {

                    idContactBitrix24 = await _bitrix24ApiService.RegistrarContactoBitrix24(
                        request.Nombre,
                        request.Apellido,
                        request.Telefono,
                        request.Email,
                        enumCampignOrigin,
                        vendedorAsignado.BitrixID
                        );


                    idMaestroProspecto = await _maestroProspectoRepository.EstablerDatosMinimoYRegistrarMaestroProspecto(_mapper.Map<MaestroProspecto>(request), idContactBitrix24);
                    seDebeIngresarProspecto = true;
                }
                else
                {
                    idContactBitrix24 = existeMaestroProspecto.BitrixID;
                    idMaestroProspecto = existeMaestroProspecto.MaeId;
                    (seDebeIngresarProspecto, prospectoIngresado) = await _prospectoRepository.VerificarIngresoProspecto(existeMaestroProspecto.MaeId);
                }
                var idProspecto = 0;

                if (seDebeIngresarProspecto)
                {
                    var idDealBitrix24 = await _bitrix24ApiService.RegistrarDealBitrix24(
                                                            tipoNegociacion,
                                                            request.Anuncio,
                                                            idContactBitrix24,
                                                            enumCampignOrigin,
                                                            vendedorAsignado.BitrixID,
                                                            null,
                                                            null,
                                                            null,
                                                            nombreDirector,
                                                            nombreGerenteZona
                                                        );
                    idProspecto = await _prospectoRepository.EstablecerDatosMinimosYRegistrarProspecto(_mapper.Map<Prospectos>(request), idMaestroProspecto, idDealBitrix24, vendedorAsignado, vendedorAsignado.ZonId, "OV12");
                }
                else
                {
                    var prospecto = await _prospectoRepository.ObtenerUltimoProspectoPorIdMaestroProspecto(existeMaestroProspecto.MaeId);
                    if (prospecto != null)
                    {
                        await _bitrix24ApiService.ValidarExistenciaDealEnBitrix(prospecto.ProId);
                    }
                    if (request.EsMasivo)
                    {
                        throw new ApplicationException($"Lead Repetido");

                    }
                }

                return idProspecto;

            }
            catch (System.Exception e)
            {
                var mensaje =$"Error ocurrido en RegistrarProspectoRedesSociales --- {e.Message}";
                var log = new LogFondos()
                {
                    Fecha = DateTime.Now,
                    Mensaje = mensaje,
                    Tipo = "Polen",
                    Valor = JsonSerializer.Serialize(request)
                };
                await _unitOfWork.Repository<LogFondos>().AddAsync(log);

                if (request.EsMasivo)
                {
                    await Task.Delay(TimeSpan.FromSeconds(120));
                    return 0;
                }
                else
                {
                    throw new ApplicationException(e.Message);
                }
            }

        }
       

        private int getOriginCapaña(string plataforma,string anuncio)
        {
            switch (anuncio)
            {
                case var _ when anuncio == CampaignOriginEnum.FORMULARIO_HOME_WEB.GetDescription():
                    return (int)CampaignOriginEnum.FORMULARIO_HOME_WEB;
                default:
                    break;
            }

            char delimiter = '_';
            string[] anuncioSplit = anuncio.Split(delimiter);
            var ciudad = anuncioSplit[0].Trim().ToUpper();
            var producto = anuncioSplit[1].Trim().ToUpper();

            var campaignString = $"{ciudad}_{producto}_{plataforma}";
            return (int)EnumDictionaryProvider.CampaignOriginEnumDict.FirstOrDefault(pair => pair.Value == campaignString).Key;

        }
        private int getIdZonaByAnuncio(string anuncio)
        {
            char delimiter = '_';
            string[] anuncioSplit = anuncio.Split(delimiter);
            var ciudad = anuncioSplit[0].Trim().ToUpper();
            return  (int)EnumDictionaryProvider.ZonaEnumDict.FirstOrDefault(pair => pair.Value == ciudad).Key;

        }
        


    }

}
