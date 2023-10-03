namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("RequestResponseLog", Schema = "appasrdir")]
    public class RequestResponseLog
    {
        public int Id { get; set; }

        public DateTime? Timestamp { get; set; }

        public string? RequestMethod { get; set; }

        public string? RequestUrl { get; set; }

        public string? RequestHeaders { get; set; }

        public string? RequestBody { get; set; }

        public int? ResponseStatus { get; set; }

        public string? ResponseHeaders { get; set; }

        public string? ResponseBody { get; set; }

        public TimeSpan? ElapsedTime { get; set; }

        public string? UserAgent { get; set; }
    }
}
