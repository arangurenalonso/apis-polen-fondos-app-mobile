namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("puntoventas")]
    public class PuntoDeVenta
    {
        [Column("pvta_cod", TypeName = "varchar")]
        public string PvtaCod { get; set; }

        [Column("pvta_dep", TypeName = "varchar")]
        public string? PvtaDep { get; set; }

        [Column("pvta_des", TypeName = "varchar")]
        public string? PvtaDes { get; set; }

        [Column("pvta_dir", TypeName = "varchar")]
        public string? PvtaDir { get; set; }

        [Column("pvta_dis", TypeName = "varchar")]
        public string? PvtaDis { get; set; }

        [Column("pvta_email", TypeName = "varchar")]
        public string? PvtaEmail { get; set; }

        [Column("pvta_est", TypeName = "bit")]
        public bool PvtaEst { get; set; }

        [Column("pvta_horario", TypeName = "varchar")]
        public string? PvtaHorario { get; set; }

        [Column("pvta_lat", TypeName = "decimal")]
        public decimal? PvtaLat { get; set; }

        [Column("pvta_long", TypeName = "decimal")]
        public decimal? PvtaLong { get; set; }

        [Column("pvta_pro", TypeName = "varchar")]
        public string? PvtaPro { get; set; }

        [Column("pvta_tele", TypeName = "varchar")]
        public string? PvtaTele { get; set; }

        [Column("pvta_zon", TypeName = "int")]
        public int? PvtaZon { get; set; }
    }
}
