namespace Infrastructure.Repositories.ApisExternas
{
    using Application.Contracts.ApiExterna;
    using Application.Contracts.ApiExterna.Abstractions;
    using Application.Models.ConsumoApi.Bitrix24.Entities;
    using Application.Models.ConsumoApi.Bitrix24.Models;
    using Application.Models.ConsumoApi.Models;
    using Infrastructure.Options.ApiExternas;
    using Microsoft.Extensions.Options;

    public class Bitrix24ApiService : IBitrix24ApiService
    {
        private readonly IClienteProvider _clienteProvider;
        private readonly ApisExternasOption _apiExtenasOptions;

        private string _url;
        private string _initialPath;
        public Bitrix24ApiService(
            IClienteProvider clienteProvider,
            IOptions<ApisExternasOption> ApiExtenasOptions)
        {
            _clienteProvider = clienteProvider;

            _apiExtenasOptions = ApiExtenasOptions.Value;
            _url = _apiExtenasOptions.Bitrix24.Production.BaseUrl;
            _initialPath = _apiExtenasOptions.Bitrix24.Production.InitialPath;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMContactAdd(ApiRequestBitrixCreate<ContactBitrix24> request)
        {
            var path = $"{_initialPath}/crm.contact.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }

        public async Task<ResponseModel<ApiResponseBitrix<string>>> CRMContactUpdate(UpdateBitrixModel<ContactBitrix24> request)
        {
            var path = $"{_initialPath}/crm.contact.update";

            ResponseModel<ApiResponseBitrix<string>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<string>>(_url, path, request);

            return response;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMUserAdd(ApiRequestBitrixCreate<UserBitrix24> request)
        {
            var path = $"{_initialPath}/user.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMDealAdd(ApiRequestBitrixCreate<DealBitrix24> request)
        {
            var path = $"{_initialPath}/crm.deal.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }
        public async Task<ResponseModel<ApiResponseBitrix<int>>> CRMLeadAdd(ApiRequestBitrixCreate<LeadBitrix24> request)
        {
            var path = $"{_initialPath}/crm.lead.add";

            ResponseModel<ApiResponseBitrix<int>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<int>>(_url, path, request);

            return response;
        }
        public async Task<ResponseModel<ApiResponseBitrix<string>>> CRMLeadUpdate(UpdateBitrixModel<LeadBitrix24> request)
        {
            var path = $"{_initialPath}/crm.lead.update";

            ResponseModel<ApiResponseBitrix<string>> response =
                await _clienteProvider.PostAsyncJson<ApiResponseBitrix<string>>(_url, path, request);

            return response;
        }
    }

}
