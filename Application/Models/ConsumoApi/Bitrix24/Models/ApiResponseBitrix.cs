namespace Application.Models.ConsumoApi.Bitrix24.Models
{
    public class ApiResponseBitrix<T>
    {
        public T Result { get; set; }
        public string? Next { get; set; }
        public string? Total { get; set; }
        public TimeData Time { get; set; }
    }
}
