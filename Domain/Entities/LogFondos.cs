namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("logfondos")]
    public class LogFondos
    {
        [Column("logid", TypeName = "int")]
        public int Logid { get; set; }

        [Column("fecha", TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [Column("mensaje", TypeName = "varchar")]
        public string? Mensaje { get; set; }

        [Column("tipo", TypeName = "varchar")]
        public string? Tipo { get; set; }

        [Column("valor", TypeName = "varchar")]
        public string? Valor { get; set; }
    }
}
