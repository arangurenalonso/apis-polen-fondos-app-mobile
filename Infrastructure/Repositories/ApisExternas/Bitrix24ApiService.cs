namespace Infrastructure.Repositories.ApisExternas
{
    using Application.Contracts.ApiExterna;
    using Application.Contracts.ApiExterna.Abstractions;
    using Application.Contracts.Repositories;
    using Application.Contracts.Repositories.Base;
    using Application.Exception;
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using Application.Models.ConsumoApi.Bitrix24.Models;
    using Application.Models.ConsumoApi.Models;
    using Domain.Entities;
    using Domain.Enum;
    using Domain.Enum.Dictionario;
    using Infrastructure.Options.ApiExternas;
    using Irony.Parsing;
    using Microsoft.Extensions.Options;

    public class Bitrix24ApiService : IBitrix24ApiService
    {
        private readonly IClienteProvider _clienteProvider;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMaestroProspectoRepository _maestroProspectoRepository;
        private readonly IProspectoRepository _prospectoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApisExternasOption _apiExtenasOptions;

        private string _url;
        private string _initialPath;
        public Bitrix24ApiService(
            IClienteProvider clienteProvider,
            IOptions<ApisExternasOption> ApiExtenasOptions,
            IVendedorRepository vendedorRepository,
            IMaestroProspectoRepository maestroProspectoRepository,
            IProspectoRepository prospectoRepository,
            IUnitOfWork unitOfWork)
        {
            _clienteProvider = clienteProvider;
            _vendedorRepository = vendedorRepository;
            _maestroProspectoRepository = maestroProspectoRepository;
            _prospectoRepository = prospectoRepository;
            _unitOfWork = unitOfWork;
            _apiExtenasOptions = ApiExtenasOptions.Value;
            _url = _apiExtenasOptions.Bitrix24.Production.BaseUrl;
            _initialPath = _apiExtenasOptions.Bitrix24.Production.InitialPath;
        }
        #region Consumo Apis Bitrix

        public async Task<ContactBitrix24> CRMContactGet(string? idContact)
        {
            var path = $"{_initialPath}/crm.contact.get";
            var request = new { id = idContact };
            ResponseModel<ApiResponseBitrix<ContactBitrix24>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<ContactBitrix24>>(_url, path, request);

            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMContactAdd(ApiRequestBitrixCreate<ContactBitrix24> request)
        {
            var path = $"{_initialPath}/crm.contact.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }

        public async Task<string> CRMContactUpdate(UpdateBitrixModel<ContactBitrix24> request)
        {
            var path = $"{_initialPath}/crm.contact.update";

            ResponseModel<ApiResponseBitrix<string>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<string>>(_url, path, request);
            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMUserAdd(ApiRequestBitrixCreate<UserBitrix24> request)
        {
            var path = $"{_initialPath}/user.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }

        public async Task<DealBitrix24> CRMDealGet(string? id)
        {
            var path = $"{_initialPath}/crm.deal.get";
            var request = new { ID = id };
            ResponseModel<ApiResponseBitrix<DealBitrix24>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<DealBitrix24>>(_url, path, request);
            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<int> CRMDealAdd(ApiRequestBitrixCreate<DealBitrix24> request)
        {
            var path = $"{_initialPath}/crm.deal.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<string> CRMDealUpdate(UpdateBitrixModel<DealBitrix24> request)
        {
            var path = $"{_initialPath}/crm.deal.update";

            ResponseModel<ApiResponseBitrix<string>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<string>>(_url, path, request);
            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }


        #endregion

        public async Task<bool> DesactivarUsuarioBitrix(string? bitrixID)
        {
            var path = $"{_initialPath}/crm.deal.update";

            var request = new
            {
                id = bitrixID,
                ACTIVE = "N"
            };

            ResponseModel<ApiResponseBitrix<bool>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<bool>>(_url, path, request);
            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<List<DealBitrix24>> CRMDealList(Dictionary<string, object> filter)
        {
            var path = $"{_initialPath}/crm.deal.list";

            var request = new
            {
                filter = filter
            };
            ResponseModel<ApiResponseBitrix<List<DealBitrix24>>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<List<DealBitrix24>>>(_url, path, request);
            if (!response.IsSuccess)
            {
                throw new ConsumoApisInternasException($"{_url}{path}", response.Errores);
            }
            return response.Result.Result;
        }
        public async Task<DealBitrix24> ValidarExistenciaDealEnBitrix(int prospectoId)
        {
            var prospecto=await _prospectoRepository.ObtenerProspectoPorId(prospectoId);
            var vendedorAsignado = await _vendedorRepository.ObtenerVendedorPorCodigo(prospecto.VenCod);
            var (existeDealEnBitrix, dealBitrixExiste) = await ValdiarExistenciaDeDealEnBitrix(prospecto.BitrixID);
            if (existeDealEnBitrix)
            {
                var origenBitrix = ObtenerOrigenFromApp(
                                        prospecto.Corivta,
                                        prospecto.ZonId, 
                                        prospecto.MedId,
                                        prospecto.ProCom, 
                                        dealBitrixExiste.SOURCE_ID
                                        );
                await ActualizarDealBitrix24(
                                         dealBitrixExiste,
                                         vendedorAsignado.BitrixID,
                                         prospecto.MotdesId,
                                         prospecto.EstId,
                                         prospecto.EstContactoId,
                                         origenBitrix
                                    );
                return dealBitrixExiste;
            }
            else
            {
                var maestroProspecto = await _maestroProspectoRepository.ObtenerMaestroProspectoPorId(prospecto.MaeId);

                var (existeContactoEnBitrix, contactoBitrixExiste) = await ValdiarExistenciaDeContactEnBitrix(maestroProspecto.BitrixID);
                if (existeContactoEnBitrix)
                {
                    Dictionary<string, object> filtro = new Dictionary<string, object>{
                        { "=CONTACT_ID", maestroProspecto.BitrixID  }
                    };
                    var listaDeals = await CRMDealList(filtro);

                    if (listaDeals.Count == 0)
                    {
                        var idDealBitrix24 = await RegistrarDealBitrix24(
                                                                prospecto.ProCom,
                                                                maestroProspecto.BitrixID,
                                                                contactoBitrixExiste.SOURCE_ID,
                                                                vendedorAsignado.BitrixID,
                                                                prospecto.MotdesId,
                                                                prospecto.EstId,
                                                                prospecto.EstContactoId
                                                            );
                        prospecto.BitrixID = idDealBitrix24;
                        await _unitOfWork.Repository<Prospectos>().UpdateAsync(prospecto);

                        return await CRMDealGet(idDealBitrix24);
                    }
                    else
                    {
                        var prospectosConMaeId = await _unitOfWork.Repository<Prospectos>().GetAsync(x=>x.MaeId==maestroProspecto.MaeId);
                        var bitrixIdsDeProspectos = prospectosConMaeId.Select(x => x.BitrixID).ToList();

                        var bitrixId = listaDeals.Select(x => x.ID.ToString()).ToList();

                        var bitrixIdsQueNoEstanEnProspectosConMaeId =
                                       bitrixId.Except(bitrixIdsDeProspectos).ToList();
                        var ultimoBitrixIdOrDefault = bitrixIdsQueNoEstanEnProspectosConMaeId.LastOrDefault();
                        if (ultimoBitrixIdOrDefault==null)
                        {
                            var idDealBitrix24 = await RegistrarDealBitrix24(
                                                                   prospecto.ProCom,
                                                                   maestroProspecto.BitrixID,
                                                                   contactoBitrixExiste.SOURCE_ID,
                                                                   vendedorAsignado.BitrixID,
                                                                   prospecto.MotdesId,
                                                                   prospecto.EstId,
                                                                   prospecto.EstContactoId
                                                               );
                            prospecto.BitrixID = idDealBitrix24;
                            await _unitOfWork.Repository<Prospectos>().UpdateAsync(prospecto);

                            return await CRMDealGet(idDealBitrix24);
                        }
                        else
                        {
                            var dealBitrixToUpdate= listaDeals.Where(x=>x.ID.ToString()== ultimoBitrixIdOrDefault).FirstOrDefault();
                            if (dealBitrixToUpdate != null)
                            {
                                var origenBitrix = ObtenerOrigenFromApp(
                                                        prospecto.Corivta,
                                                        prospecto.ZonId,
                                                        prospecto.MedId,
                                                        prospecto.ProCom,
                                                        dealBitrixToUpdate.SOURCE_ID
                                                        );
                                await ActualizarDealBitrix24(
                                                         dealBitrixToUpdate,
                                                         vendedorAsignado.BitrixID,
                                                         prospecto.MotdesId,
                                                         prospecto.EstId,
                                                         prospecto.EstContactoId,
                                                         origenBitrix
                                                    );
                            }

                            await ActualizarContactoBitrix24(
                                                         contactoBitrixExiste,
                                                         vendedorAsignado.BitrixID
                                                    );

                            
                            prospecto.BitrixID = ultimoBitrixIdOrDefault;
                            await _unitOfWork.Repository<Prospectos>().UpdateAsync(prospecto);
                            return await CRMDealGet(ultimoBitrixIdOrDefault);

                        }
                    }
                }
                else
                {
                    var campanaOrigen = "";
                    var idContactBitrix24 = await RegistrarContactoBitrix24(
                       maestroProspecto.MaeNom,
                       maestroProspecto.MaePat,
                       maestroProspecto.MaeCel1,
                       maestroProspecto.MaeEmail,
                       campanaOrigen,
                       vendedorAsignado.BitrixID
                       );
                    maestroProspecto.BitrixID = idContactBitrix24;
                    await _unitOfWork.Repository<MaestroProspecto>().UpdateAsync(maestroProspecto);

                    var idDealBitrix24 = await RegistrarDealBitrix24(
                                                            prospecto.ProCom,
                                                            idContactBitrix24,
                                                            campanaOrigen,
                                                            vendedorAsignado.BitrixID,
                                                            prospecto.MotdesId,
                                                            prospecto.EstId,
                                                            prospecto.EstContactoId
                                                        );
                    prospecto.BitrixID = idDealBitrix24;
                    await _unitOfWork.Repository<Prospectos>().UpdateAsync(prospecto);

                    return await CRMDealGet(idDealBitrix24);
                }
               
            }
        }

        private string ObtenerOrigenFromApp(string corivta, int? zonaId, int? medId, string? proCom, string? bitrixSourceId)
        {
            if(corivta== "OV12")
            {
                var sourceIDNuevo = "";
                var ciudad = "";
                if (zonaId != null)
                {
                    switch (zonaId)
                    {
                        case 1:
                            ciudad = "BOGOTA";
                            break;
                        case 2:
                            ciudad = "MEDELLIN";
                            break;
                        case 3:
                            ciudad = "ANTIOQUIA";
                            break;
                        default:
                            break;
                    }
                }
                var plataforma = "";
                if (medId != null)
                {
                    switch (medId)
                    {
                        case 1:
                            plataforma = "META";
                            break;
                        case 25:
                            plataforma = "TIKTOK";
                            break;
                        default:
                            break;
                    }
                }
                var producto = "";

                if (proCom != null)
                {
                    if (proCom.Contains("_AUTO_"))
                    {
                        producto = "AUTO";
                    }
                    if (proCom.Contains("_SEMINUEVO_"))
                    {
                        producto = "SEMINUEVO";
                    }
                }
                if (string.IsNullOrEmpty(ciudad) ||
                    string.IsNullOrEmpty(plataforma) ||
                    string.IsNullOrEmpty(producto))
                {
                    return bitrixSourceId;
                }
                else
                {
                    var campaignString = $"{ciudad}_{producto}_{plataforma}";
                    return ((int)EnumDictionaryProvider.CampaignOriginEnumDict.FirstOrDefault(pair => pair.Value == campaignString).Key).ToString();
                }

            }
            else if (corivta == "OV02")
            {
                return "1";
            }
            else if (corivta == "OV03")
            {
                return "66";

            }
            else if (corivta == "OV04")
            {
                return "65";
            }
            else
            {
                return bitrixSourceId;
            }     
        }

        public async Task<string> RegistrarDealBitrix24(
            string anuncio, 
            string idContactBitrix24,
            string enumCampignOrigin,
            string idUsuarioBitrix,
            int? motDesId=null,
            int? estId=null,
            int? estContactoId=null)
        {
            var obj = new DealBitrix24
            {
                TITLE = anuncio,
                CONTACT_ID = idContactBitrix24,
                SOURCE_ID = enumCampignOrigin,
                CATEGORY_ID = "8",
                UF_CRM_1695762208 = ((int)EstadoDealBitrixEnum.NoContactado).ToString(),
                ASSIGNED_BY_ID=idUsuarioBitrix,
                UF_CRM_1695762237 = "",
                UF_CRM_1695762254 = "",
            };
            if (motDesId!=null || estId != null || estContactoId != null )
            {
                obj =await  SetarEstado(obj, motDesId, estId, estContactoId);
            }

            var request = new ApiRequestBitrixCreate<DealBitrix24>()
            {
                fields = obj,
                Params = new ParamsApiRequestCreate()
                {
                    REGISTER_SONET_EVENT = "Y"
                }
            };
            var resultadoPost = await CRMDealAdd(request);
            return resultadoPost.ToString();
        }
        private async Task<(bool, DealBitrix24?)> ValdiarExistenciaDeDealEnBitrix(string bitrixId)
        {
            try
            {
                var dealBitrix = await CRMDealGet(bitrixId);
                return (true, dealBitrix);
            }
            catch
            {
                return (false, null);
            }
        }
        public async Task<string> RegistrarContactoBitrix24(
            string nombre, 
            string? apellido, 
            string? telefono,
            string? correoElectronico,
            string campanaOrigen,
            string idUsuarioBitrix)
        {
            var obj = new ContactBitrix24
            {
                NAME = nombre,
                LAST_NAME = apellido,
                SOURCE_ID = campanaOrigen,
                ASSIGNED_BY_ID = idUsuarioBitrix
            };
            if (telefono != null)
            {
                var phone = new TypedField
                {
                    VALUE = telefono,
                    VALUE_TYPE = "MAILING",
                    TYPE_ID = "PHONE"
                };
                obj.PHONE.Add(phone);
            }
            if (correoElectronico != null)
            {
                var email = new TypedField
                {
                    VALUE = correoElectronico,
                    VALUE_TYPE = "MAILING",
                    TYPE_ID = "EMAIL"
                };
                obj.EMAIL.Add(email);
            }
            var request = new ApiRequestBitrixCreate<ContactBitrix24>()
            {
                fields = obj,
                Params = new ParamsApiRequestCreate()
                {
                    REGISTER_SONET_EVENT = "Y"
                }

            };

            var resultadoPost = await CRMContactAdd(request);
            if (!resultadoPost.IsSuccess)
            {
                throw new ApplicationException(resultadoPost.Message);
            }
            return resultadoPost.Result.Result.ToString();
        }
        private async Task<(bool, ContactBitrix24?)> ValdiarExistenciaDeContactEnBitrix(string bitrixId)
        {
            try
            {
                var contactBitrix = await CRMContactGet(bitrixId);
                return (true, contactBitrix);
            }
            catch
            {
                return (false, null);
            }
        }
        private async Task<DealBitrix24> SetarEstado(
            DealBitrix24 deal,
            int? motDesId=null,
            int? estId = null,
            int? estContactoId = null)
        {
            var discardReasons = await _unitOfWork.Repository<DiscardReasonsEntity>().GetAllAsync();
            var statesContactos = await _unitOfWork.Repository<StatesContactosEntity>().GetAllAsync();


            var estado = "";
            var razonNoContacto = "";
            var razonDescarte = "";

            if (motDesId != null)
            {
                estado = ((int)EstadoDealBitrixEnum.Descartado).ToString();
                razonNoContacto = "";
                var motivoRechazo = discardReasons.Where(x => x.Id == motDesId).FirstOrDefault();
                if (motivoRechazo != null)
                {
                    razonDescarte = motivoRechazo.Title;
                }
            }
            else
            {
                switch (estId)
                {
                    case 1://No contactado
                        estado = ((int)EstadoDealBitrixEnum.NoContactado).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    case 2://No contactado 
                        estado = ((int)EstadoDealBitrixEnum.NoContactado).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    case 3://Contactado 
                        estado = ((int)EstadoDealBitrixEnum.Contactado).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    case 5://Cita programada 
                        estado = ((int)EstadoDealBitrixEnum.CitaProgramada).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    case 6://Cita realizada 
                        estado = ((int)EstadoDealBitrixEnum.CitaRealizada).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    case 12://Inscrito 
                        estado = ((int)EstadoDealBitrixEnum.Inscrito).ToString();
                        razonNoContacto = "";
                        razonDescarte = "";
                        break;
                    default:
                        break;
                }
                if (estContactoId != null)
                {
                    var estadoContactado = statesContactos.Where(x => x.Id == estContactoId).FirstOrDefault();
                    if (estadoContactado != null)
                    {
                        razonNoContacto = estadoContactado.Title;
                    }
                }
                deal.UF_CRM_1695762208 = estado;
                deal.UF_CRM_1695762237 = razonNoContacto;
                deal.UF_CRM_1695762254 = razonDescarte;
            }
            return deal;
        }
        
        public async Task<string> ActualizarContactoBitrix24(
          ContactBitrix24 contactBitrix24,
          string idUsuarioBitrix
          )
        {
            contactBitrix24.ASSIGNED_BY_ID = idUsuarioBitrix;

            var request = new UpdateBitrixModel<ContactBitrix24>()
            {
                id = contactBitrix24.ID,
                fields = contactBitrix24

            };

            var result = await CRMContactUpdate(request);
            return result;
        }
        public async Task<string> ActualizarDealBitrix24(
            DealBitrix24 dealBitrix24, 
            string idUsuarioBitrix,
            int? motDesId = null,
            int? estId = null,
            int? estContactoId = null,
            string? origenBitrix=null)
        {

            dealBitrix24.ASSIGNED_BY_ID = idUsuarioBitrix;
            if (origenBitrix!=null)
            {
                dealBitrix24.SOURCE_ID = origenBitrix;
            }
            if (motDesId != null || estId != null || estContactoId != null)
            {
                dealBitrix24 = await SetarEstado(dealBitrix24, motDesId, estId, estContactoId);
            }
            var request = new UpdateBitrixModel<DealBitrix24>()
            {
                id = dealBitrix24.ID.ToString(),
                fields = dealBitrix24
            };

            var result = await CRMDealUpdate(request);
            return result;
        }

      
    }

}
