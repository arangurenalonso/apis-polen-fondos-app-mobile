namespace Presentation.Models
{
    public class TokenResult
    {
        public bool IsValid { get; set; } = true;
        public string? ErrorMessage { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? NumDocumento { get; set; }
    }
}
