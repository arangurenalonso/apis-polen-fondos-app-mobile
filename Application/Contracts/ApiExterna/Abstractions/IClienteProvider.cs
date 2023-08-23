namespace Application.Contracts.ApiExterna.Abstractions
{
    using Application.Models.ConsumoApi.Models;
    using Microsoft.AspNetCore.Http;

    public interface IClienteProvider
    {
        Task<ResponseModel<T>> GetAsync<T>(string urlBase, string path, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> GetAsyncWithQueryParams<T>(string urlBase, string path, Dictionary<string, string> queryParams, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> PostAsyncFormData<T>(string urlBase, string path, Dictionary<string, string> formData, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> PostAsyncFormData<T>(string urlBase, string path, Dictionary<string, string> formData, IFormFile file, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> PostAsyncJson<T>(string urlBase, string path, Object model, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> UploadFileAsync<T>(string urlBase, string path, string filePath, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> PutAsync<T>(string urlBase, string path, Object model, int id, string? tokenType = null, string? accessToken = null);
        Task<ResponseModel<T>> DeleteAsync<T>(string urlBase, string path, string? tokenType = null, string? accessToken = null);
    }
}
