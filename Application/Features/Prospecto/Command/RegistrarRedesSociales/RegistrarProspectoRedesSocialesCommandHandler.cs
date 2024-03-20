namespace Application.Features.Prospecto.Command.RegistrarRedesSociales
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
    using Application.Helper;

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
                if (request.NumeroRegistro==1 && request.EsMasivo)
                {
                    var mensaje = $"Inicio - Ejecución Proceso Registro Prospecto Redes Sociales Masivo";
                    var log = new LogFondos()
                    {
                        Fecha = DateTime.Now,
                        Mensaje = mensaje,
                        Tipo = "Polen",
                        Valor = JsonSerializer.Serialize(request.NumeroRegistro)
                    };
                    await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                }
                if (request.NumeroRegistro % 25 == 0)
                {
                    var mensaje = $"Progreso - Ejecución Proceso Registro Prospecto Redes Sociales Masivo";
                    var log = new LogFondos()
                    {
                        Fecha = DateTime.Now,
                        Mensaje = mensaje,
                        Tipo = "Polen",
                        Valor = JsonSerializer.Serialize(request.NumeroRegistro)
                    };
                    await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                }
                if (request.EsUltimoRegistro)
                {
                    var mensaje = $"Finalización - Ejecución Proceso Registro Prospecto Redes Sociales Masivo";
                    var log = new LogFondos()
                    {
                        Fecha = DateTime.Now,
                        Mensaje = mensaje,
                        Tipo = "Polen",
                        Valor = JsonSerializer.Serialize(request.NumeroRegistro)
                    };
                    await _unitOfWork.Repository<LogFondos>().AddAsync(log);
                }
                
                var tipoNegociacion =
                    EnumDictionaryProvider.TipoNegociacionEnumDict[TipoNegociacionEnum.VENTA_DIGITAL_B2C];

                var (enumCampignOrigin, zonaId) = getOriginAndIdZonaByCapaña(request.Plataforma!, request.Anuncio!);
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
                    if (request.VaBitrix)
                    {
                        idContactBitrix24 = await _bitrix24ApiService.RegistrarContactoBitrix24(
                            request.Nombre,
                            request.Apellido,
                            request.Telefono,
                            request.Email,
                            enumCampignOrigin.ToString(),
                            vendedorAsignado.BitrixID
                            );
                    }

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
                    var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("America/Lima");
                    var fechaActual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                    var fechaString = fechaActual.ToString();
                    var idDealBitrix24 = "";
                    if (request.VaBitrix)
                    {
                        idDealBitrix24 = await _bitrix24ApiService.RegistrarDealBitrix24(
                                                           tipoNegociacion,
                                                           request.Anuncio,
                                                           idContactBitrix24,
                                                           enumCampignOrigin.ToString(),
                                                           vendedorAsignado.BitrixID,
                                                          fechaString,
                                                           fechaString,
                                                           null,
                                                           null,
                                                           null,
                                                           nombreDirector,
                                                           nombreGerenteZona,
                                                           request.Anuncio

                                                       );
                    }
                       
                    idProspecto = await _prospectoRepository.EstablecerDatosMinimosYRegistrarProspecto(_mapper.Map<Prospectos>(request), 
                        idMaestroProspecto, idDealBitrix24, vendedorAsignado, vendedorAsignado.ZonId, "OV12", enumCampignOrigin.ToString()
                        , fechaActual, fechaActual
                        );
                }
                else
                {
                    var prospecto = await _prospectoRepository.ObtenerUltimoProspectoPorIdMaestroProspecto(existeMaestroProspecto.MaeId);
                    if (prospecto != null)
                    {
                        if (request.VaBitrix)
                        {
                            await _bitrix24ApiService.ValidarExistenciaDealEnBitrix(prospecto.ProId);

                        }

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
                    if (request.VaBitrix)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(120));
                    }
                    
                    return 0;
                }
                else
                {
                    throw new ApplicationException(e.Message);
                }
            }

        }
       

        private (int,int) getOriginAndIdZonaByCapaña(string plataforma,string anuncio)
        {
            switch (anuncio)
            {
                case var _ when anuncio == CampaignOriginEnum.FORMULARIO_HOME_WEB.GetDescription():
                    return ((int)CampaignOriginEnum.FORMULARIO_HOME_WEB,0);
                default:
                    break;
            }

            var ciudad = "";
            var palabrasBuscadaMedellin = new List<string> { "MEDELLIN", "ANTIOQUIA", "Medellín" };
            if (EvaluarOracion(anuncio, palabrasBuscadaMedellin))
            {
                ciudad = "MEDELLIN";
            }
            var palabrasBuscadaBogota = new List<string> { "BOGOTA", "Bogotá" };
            if (EvaluarOracion(anuncio, palabrasBuscadaBogota))
            {
                ciudad = "BOGOTA";
            }

            var palabrasBuscadaBarranquilla = new List<string> { "BARRANQUILLA", "Barranquilla" };
            
            if (EvaluarOracion(anuncio, palabrasBuscadaBarranquilla))
            {
                ciudad = "BARRANQUILLA";
            }


            var zonaId = (int)EnumDictionaryProvider.ZonaEnumDict.FirstOrDefault(pair => pair.Value == ciudad).Key;

            var anuncioBitrixId = getCampaingBitrixIdFromAdName(plataforma, anuncio);
            if (anuncioBitrixId!=0)
            {
                return (anuncioBitrixId, zonaId);
            }

            var producto = "";
            var palabrasBuscadaAutoNuevo = new List<string> { "AUTO", "NUEVO" };
            if (EvaluarOracion(anuncio, palabrasBuscadaAutoNuevo))
            {
                producto = "AUTO";
            }
            var palabrasBuscadaAutoUsado = new List<string> { "SEMINUEVOS", "USADO" };
            if (EvaluarOracion(anuncio, palabrasBuscadaAutoUsado))
            {
                producto = "SEMINUEVO";
            }

            var campaignString = $"{ciudad}_{producto}_{plataforma}";
            return ((int)EnumDictionaryProvider.CampaignOriginEnumDict.FirstOrDefault(pair => pair.Value == campaignString).Key,
                 zonaId
                );

        }
        private int getCampaingBitrixIdFromAdName(string plataforma, string anuncio)
        {
            if (string.IsNullOrWhiteSpace(plataforma) || string.IsNullOrWhiteSpace(anuncio))
            {
                return 0; 
            }
            var campaignIdMap = MethodHelper.InitializeCampaignIdMap(plataforma); 

            if (campaignIdMap.TryGetValue(anuncio.ToUpper().Trim(), out int campaignBitrixId))
            {
                return campaignBitrixId;
            }

            return 0;
        }

        //private int getCampaingBitrixIdFromAdName(string plataforma, string anuncio)
        //{
        //    var campaignBitrixId = 0;

        //    if (plataforma.ToUpper().Trim()== PlataformaEnum.META.GetDescription())
        //    {
        //        switch (anuncio)
        //        {
        //            case var _ when anuncio == CampaignOriginEnumMETA.BARRANQUILLA_CARRO_NUEVO_TEMATICA.GetDescription():
        //                campaignBitrixId=(int)CampaignOriginEnumMETA.BARRANQUILLA_CARRO_NUEVO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BARRANQUILLA_CARRO_USADO_TEMATICA.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BARRANQUILLA_CARRO_USADO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BARRANQUILLA_CARRO_NUEVO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BARRANQUILLA_CARRO_NUEVO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BARRANQUILLA_CARRO_USADO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BARRANQUILLA_CARRO_USADO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BARRANQUILLA_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BARRANQUILLA_CARRO_REEL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BOGOTA_CARRO_NUEVO_TEMATICA.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BOGOTA_CARRO_NUEVO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BOGOTA_CARRO_USADO_TEMATICA.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BOGOTA_CARRO_USADO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BOGOTA_CARRO_NUEVO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BOGOTA_CARRO_NUEVO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BOGOTA_CARRO_USADO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BOGOTA_CARRO_USADO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.BOGOTA_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.BOGOTA_CARRO_REEL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.MEDELLIN_CARRO_NUEVO_TEMATICA.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.MEDELLIN_CARRO_NUEVO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.MEDELLIN_CARRO_USADO_TEMATICA.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.MEDELLIN_CARRO_USADO_TEMATICA;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.MEDELLIN_CARRO_NUEVO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.MEDELLIN_CARRO_NUEVO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.MEDELLIN_CARRO_USADO_COMERCIAL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.MEDELLIN_CARRO_USADO_COMERCIAL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumMETA.MEDELLIN_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumMETA.MEDELLIN_CARRO_REEL;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    if (plataforma.ToUpper().Trim() == PlataformaEnum.TIKTOK.GetDescription())
        //    {
        //        switch (anuncio)
        //        {
        //            case var _ when anuncio == CampaignOriginEnumTIKTOK.BARRANQUILLA_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumTIKTOK.BARRANQUILLA_CARRO_REEL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumTIKTOK.BOGOTA_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumTIKTOK.BOGOTA_CARRO_REEL;
        //                break;
        //            case var _ when anuncio == CampaignOriginEnumTIKTOK.MEDELLIN_CARRO_REEL.GetDescription():
        //                campaignBitrixId = (int)CampaignOriginEnumTIKTOK.MEDELLIN_CARRO_REEL;
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    return campaignBitrixId;
        //}

        private bool EvaluarOracion(string oracion, List<string> palabrasVascas)
        {
            foreach (var palabra in palabrasVascas)
            {
                if (oracion.ToUpper().Contains(palabra.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

    }

}
