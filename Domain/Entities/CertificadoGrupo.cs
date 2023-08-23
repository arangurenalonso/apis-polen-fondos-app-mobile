namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("certificadogrupos")]
    public class CertificadoGrupo
    {
        [ForeignKey("Grupo")]
        [Column("cgru_id", TypeName = "varchar(4)")]
        public string GrupoId { get; set; }

        [Column("ccer_id")]
        [ForeignKey("Certificado")]
        public string CertificadoId { get; set; }
        [Column("ccer_mon")]
        public Decimal Monto { get; set; }
        [Column("ccer_madj")]
        public Decimal? CuotaALaAdjudicacion { get; set; }
        [Column("ccer_mpro")]
        public Decimal? CuotaProrrateada { get; set; }
        [Column("ccer_pcins")]
        public Decimal? PorcentajeCuotaInscripcion { get; set; }
        [Column("ccer_mcins")]
        public Decimal? MontoCuotaInscripcion { get; set; }
    }
}
