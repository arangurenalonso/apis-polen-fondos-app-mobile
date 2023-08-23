namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("states")]
    public class Estados
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("title", TypeName = "varchar")]
        public string? Title { get; set; }

        [Column("created_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }

        [Column("idmaquilink", TypeName = "int")]
        public int? IdMaquilink { get; set; }
    }
}
