namespace Application.Exception
{
    using Application.Models.ConsumoApi.Models;
    using Newtonsoft.Json;
    public class ConsumoApisInternasException : ApplicationException
    {
        public string Mensaje { get; set; }
        public ErrorClientProviderDetails Details { get; set; }
        public ConsumoApisInternasException(string endpoint, ErrorClientProviderDetails errores) 
            : base($"Message: {errores.Message} - ApiResponse: {errores.ApiResponse} - RequestURL: {errores.RequestUrl} - RequestMethod: {errores.RequestMethod} - RequestBody: {errores.RequestBody} - HttpStatusCode: {errores.HttpStatusCode}")
        {
            Mensaje = $"Se produjo un error al intentar conectar con el servicio API '{endpoint}'";
            Details = errores;
        }

    }
}
