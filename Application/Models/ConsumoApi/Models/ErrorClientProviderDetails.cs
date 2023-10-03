namespace Application.Models.ConsumoApi.Models
{
    public class ErrorClientProviderDetails
    { 
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? RequestUrl { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestBody { get; set; }
        public string? ApiResponse { get; set; }
        public int HttpStatusCode { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
