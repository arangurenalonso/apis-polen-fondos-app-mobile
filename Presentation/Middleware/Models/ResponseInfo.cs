namespace Presentation.Middleware.Models
{
    public class ResponseInfo
    {
        public string? ContentType { get; set; }
        public string? TextContent { get; set; }
        public long? Size { get; set; }
        public string? FileName { get; set; }
    }
}
