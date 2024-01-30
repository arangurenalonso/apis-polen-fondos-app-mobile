namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("states", Schema = "appasrdir")]
    public class StateEntity
    {
        [Key]
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("title", TypeName = "varchar")]
        public string? Title { get; set; }
        [Column("idmaquilink", TypeName = "int")]
        public int? IdMaquiLink { get; set; }
        [Column("created_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
    }
}
