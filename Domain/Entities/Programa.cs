namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    [Table("programas")]
    public class Programa
    {
        [Key]
        [Column("cpro_id")]
        public string Id { get; set; }

        [Column("cpro_des")]
        public string? Descripcion { get; set; }
    }
}
