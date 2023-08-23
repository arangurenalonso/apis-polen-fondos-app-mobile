namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("grupos")]
    public partial class Grupo
    {
        [Key]
        [Column("cgru_id")]
        public string Id { get; set; }

        [Column("cgru_est")]
        public int? Estado { get; set; }
        //0 formacion
        //1 jugando
        //2 termino
        //3 liquidado
        [Column("cpro_id")]
        [ForeignKey("Grupo")]
        public string ProgramaId { get; set; }
        [Column("cgru_nasa")]
        public int? NumeroAsamblea { get; set; }
        [Column("cgru_fasa")]
        public DateTime? FechaAsamblea { get; set; }
    }
}
