namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("vendedores",Schema = "appasrdir")]
    public class Vendedores
    {
        [Key]
        [Column("ven_cod", TypeName = "varchar")]
        public string VenCod { get; set; }

        [Column("ven_numdoc", TypeName = "varchar")]
        public string? VenNumDoc { get; set; } 

        [Column("ven_nom", TypeName = "varchar")]
        public string? VenNom { get; set; }

        [Column("ven_pass", TypeName = "varchar")]
        public string? VenPass { get; set; }

        [Column("ven_carcod", TypeName = "varchar")]
        public string? VenCarCod { get; set; }

        [Column("ven_sec", TypeName = "varchar")]
        public string? VenSec { get; set; }

        [Column("ven_supcod", TypeName = "varchar")]
        public string? VenSupCod { get; set; }

        [Column("ven_gescod", TypeName = "varchar")]
        public string? VenGesCod { get; set; }

        [Column("ven_gercod", TypeName = "varchar")]
        public string? VenGerCod { get; set; }

        [Column("ven_cel", TypeName = "varchar")]
        public string? VenCel { get; set; }

        [Column("ven_imei", TypeName = "varchar")]
        public string? VenImei { get; set; }

        [Column("ven_fcese", TypeName = "datetime")]
        public DateTime?  VenFcese { get; set; }

        [Column("ven_app", TypeName = "bit")]
        public bool? VenApp { get; set; }//

        [Column("ven_equ", TypeName = "varchar")]
        public string? VenEqu { get; set; }

        [Column("ven_email", TypeName = "varchar")]
        public string? VenEmail { get; set; }

        [Column("cat_id", TypeName = "int")]
        public int? CatId { get; set; }

        [Column("can_id", TypeName = "varchar")]
        public string? CanId { get; set; }

        [Column("zon_id", TypeName = "int")]
        public int ZonId { get; set; }

        [Column("is_doc", TypeName = "int")]
        public int? IsDoc { get; set; }

        [Column("is_leads", TypeName = "int")]
        public int? IsLeads { get; set; }

        [Column("plead", TypeName = "int")]
        public int? Plead { get; set; }

        [Column("qlead", TypeName = "int")]
        public int? Qlead { get; set; }

        [Column("ven_leadDisabled", TypeName = "bit")]
        public bool? VenLeadDisabled { get; set; }

        [Column("Alias", TypeName = "varchar")]
        public string? Alias { get; set; }

        [Column("PortalAgentes", TypeName = "bit")]
        public bool? PortalAgentes { get; set; }
        [Column("BitrixID", TypeName = "LONGTEXT")]
        [JsonPropertyName("BitrixID")]
        public string? BitrixID { get; set; }


    }
}
