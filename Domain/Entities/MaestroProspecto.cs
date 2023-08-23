namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("maestroprospecto")]
    public  class MaestroProspecto
    {
        [Column("mae_id", TypeName = "int")]
        public int MaeId { get; set; }

        [Column("doc_id", TypeName = "varchar")]
        public string? DocId { get; set; }

        [Column("mae_numdoc", TypeName = "varchar")]
        public string? MaeNumdoc { get; set; }

        [Column("mae_pat", TypeName = "varchar")]
        public string? MaePat { get; set; }

        [Column("mae_mat", TypeName = "varchar")]
        public string? MaeMat { get; set; }

        [Column("mae_nom", TypeName = "varchar")]
        public string MaeNom { get; set; }

        [Column("mae_raz", TypeName = "varchar")]
        public string? MaeRaz { get; set; }

        [Column("mae_rel", TypeName = "varchar")]
        public string? MaeRel { get; set; }

        [Column("mae_dir", TypeName = "varchar")]
        public string? MaeDir { get; set; }

        [Column("dep_id", TypeName = "varchar")]
        public string? DepId { get; set; }

        [Column("pro_id", TypeName = "varchar")]
        public string? ProId { get; set; }

        [Column("dis_id", TypeName = "varchar")]
        public string? DisId { get; set; }

        [Column("mae_cel1", TypeName = "varchar")]
        public string MaeCel1 { get; set; }

        [Column("mae_cel2", TypeName = "varchar")]
        public string? MaeCel2 { get; set; }

        [Column("mae_telfijo", TypeName = "varchar")]
        public string? MaeTelfijo { get; set; }

        [Column("mae_feccrea", TypeName = "datetime")]
        public DateTime? MaeFeccrea { get; set; }

        [Column("mae_fecactu", TypeName = "datetime")]
        public DateTime? MaeFecactu { get; set; }

        [Column("mae_cneg", TypeName = "bit")]
        public bool? MaeCneg { get; set; }

        [Column("mae_email", TypeName = "varchar")]
        public string? MaeEmail { get; set; }

        [Column("Activo", TypeName = "int")]
        public int? Activo { get; set; }

        [Column("genero", TypeName = "varchar")]
        public string? Genero { get; set; }

        [Column("fnacimiento", TypeName = "datetime")]
        public DateTime? Fnacimiento { get; set; }

        [Column("BitrixID", TypeName = "LONGTEXT")]
        [JsonPropertyName("BitrixID")]
        public string? BitrixID { get; set; }

        public virtual ICollection<Prospectos>? Prospectos { get; set; }
    }
}
