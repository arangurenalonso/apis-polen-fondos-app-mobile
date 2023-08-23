namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    [Table("certificados")]
    public class Certificado
    {
        [Key]
        [Column("ccer_id")]
        public string Id { get; set; }

        [Column("ccer_mon")]
        public Decimal? Monto { get; set; }
        [Column("ccer_est")]
        public int? Estado { get; set; }
    }
}
