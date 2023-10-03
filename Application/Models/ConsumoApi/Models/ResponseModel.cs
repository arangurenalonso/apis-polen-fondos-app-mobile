namespace Application.Models.ConsumoApi.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
        public ErrorClientProviderDetails? Errores { get; set; }
    }
}
