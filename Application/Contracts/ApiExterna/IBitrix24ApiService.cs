namespace Application.Contracts.ApiExterna
{
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using Application.Models.ConsumoApi.Bitrix24.Models;
    using Application.Models.ConsumoApi.Models;
    public interface IBitrix24ApiService
    {
        Task<ResponseModel<ApiResponseBitrix<int>>> CRMContactAdd(ApiRequestBitrixCreate<ContactBitrix24> request);
        Task<ResponseModel<ApiResponseBitrix<string>>> CRMContactUpdate(UpdateBitrixModel<ContactBitrix24> request);
        Task<ResponseModel<ApiResponseBitrix<int>>> CRMUserAdd(ApiRequestBitrixCreate<UserBitrix24> request);
        Task<ResponseModel<ApiResponseBitrix<int>>> CRMDealAdd(ApiRequestBitrixCreate<DealBitrix24> request);
        Task<ResponseModel<ApiResponseBitrix<int>>> CRMLeadAdd(ApiRequestBitrixCreate<LeadBitrix24> request);
        Task<ResponseModel<ApiResponseBitrix<string>>> CRMLeadUpdate(UpdateBitrixModel<LeadBitrix24> request);
    }
}
