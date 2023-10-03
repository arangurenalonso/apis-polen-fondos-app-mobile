namespace Presentation.Middleware.Models
{
    public class RequestInfo
    {
        public string? Method { get; set; }
        public string? Scheme { get; set; }
        public string? Host { get; set; }
        public string? Path { get; set; }
        public string? QueryString { get; set; }
        public object? Body { get; set; }
    }
}
