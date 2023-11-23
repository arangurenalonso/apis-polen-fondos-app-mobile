namespace Application.Contracts.ApiExterna
{
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using Application.Models.ConsumoApi.Bitrix24.Models;
    using Application.Models.ConsumoApi.Models;
    using Domain.Entities;

    public interface IBitrix24ApiService 
    {
        Task<ContactBitrix24> CRMContactGet(string? idContact);
        Task<ResponseModel<ApiResponseBitrix<int>>> CRMContactAdd(ApiRequestBitrixCreate<ContactBitrix24> request);
        Task<string> CRMContactUpdate(UpdateBitrixModel<ContactBitrix24> request);

        Task<ResponseModel<ApiResponseBitrix<int>>> CRMUserAdd(ApiRequestBitrixCreate<UserBitrix24> request);

        Task<DealBitrix24> CRMDealGet(string? id);
        Task<int> CRMDealAdd(ApiRequestBitrixCreate<DealBitrix24> request);
        Task<string> CRMDealUpdate(UpdateBitrixModel<DealBitrix24> request);

        Task<bool> DesactivarUsuarioBitrix(string? bitrixID);
        Task<List<DealBitrix24>> CRMDealList(Dictionary<string, object> filter);
        Task<DealBitrix24> ValidarExistenciaDealEnBitrix(int prospectoId);
        Task<string> RegistrarDealBitrix24(
            string tipoNegociacion,
            string anuncio,
            string idContactBitrix24,
            string enumCampignOrigin,
            string idUsuarioBitrix,
            int? motDesId = null,
            int? estId = null,
            int? estContactoId = null,
            string? nombreDirector = null,
            string? nombreGerenteZona = null);
        Task<string> RegistrarContactoBitrix24(
            string nombre,
            string? apellido,
            string? telefono,
            string? correoElectronico,
            string campanaOrigen,
            string idUsuarioBitrix);

        Task<string> ActualizarContactoBitrix24(
          ContactBitrix24 contactBitrix24,
          string idUsuarioBitrix,
          string? campanaOrigen = null);
        Task<string> ActualizarDealBitrix24(
           DealBitrix24 dealBitrix24,
           string tipoNegociacion,
           string idUsuarioBitrix,
           int? motDesId = null,
           int? estId = null,
           int? estContactoId = null,
           string? origenBitrix = null,
           string? nombreDirector = null,
           string? nombreGerenteZona = null);
    }
}
