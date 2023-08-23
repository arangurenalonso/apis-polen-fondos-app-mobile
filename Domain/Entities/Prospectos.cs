namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("prospectos")]
    public class Prospectos
    {
        [Column("pro_id", TypeName = "int")]
        public int ProId { get; set; }

        [Column("mae_id", TypeName = "int")]
        public int MaeId { get; set; }//Foregin key

        [Column("pro_fecpro", TypeName = "datetime")]
        public DateTime ProFecpro { get; set; }

        [Column("pro_fecasi", TypeName = "datetime")]
        public DateTime? ProFecasi { get; set; }

        [Column("nlin_id", TypeName = "int")]
        public int NlinId { get; set; }//Foregin key

        [Column("int_id", TypeName = "bit")]
        public bool? IntId { get; set; }=false;

        [Column("corivta", TypeName = "varchar")]
        public string? Corivta { get; set; }

        [Column("cptovta", TypeName = "varchar")]
        public string? Cptovta { get; set; }

        [Column("eve_id", TypeName = "varchar")]
        public string? EveId { get; set; }

        [Column("med_id", TypeName = "int")]
        public int? MedId { get; set; }

        [Column("ctie_id", TypeName = "varchar")]
        public string? CtieId { get; set; }

        [Column("orimaq_id", TypeName = "varchar")]
        public string? OrimaqId { get; set; }

        [Column("ventie_id", TypeName = "int")]
        public int? VentieId { get; set; }

        [Column("cfi_id", TypeName = "int")]
        public int? CfiId { get; set; }

        [Column("suptie_id", TypeName = "int")]
        public int? SuptieId { get; set; }

        [Column("jeftie_id", TypeName = "int")]
        public int? JeftieId { get; set; }

        [Column("est_id", TypeName = "int")]
        public int EstId { get; set; }

        [Column("pro_fecest", TypeName = "datetime")]
        public DateTime ProFecest { get; set; }

        [Column("motdes_id", TypeName = "int")]
        public int? MotdesId { get; set; }

        [Column("pro_com", TypeName = "varchar")]
        public string? ProCom { get; set; }

        [Column("ven_cod", TypeName = "varchar")]
        public string? VenCod { get; set; }

        [Column("ven_supcod", TypeName = "varchar")]
        public string? VenSupcod { get; set; }

        [Column("ven_gescod", TypeName = "varchar")]
        public string? VenGescod { get; set; }

        [Column("ven_gercod", TypeName = "varchar")]
        public string? VenGercod { get; set; }

        [Column("ven_obsaprob", TypeName = "varchar")]
        public string? VenObsaprob { get; set; }

        [Column("tipo_persona", TypeName = "int")]
        public int? TipoPersona { get; set; }

        [Column("ori_id", TypeName = "int")]
        public int? OriId { get; set; }

        [Column("zon_id", TypeName = "int")]
        public int? ZonId { get; set; }

        [Column("last_payment_code", TypeName = "varchar")]
        public string? LastPaymentCode { get; set; }

        [Column("est_llamada_id", TypeName = "int")]
        public int? EstLlamadaId { get; set; }

        [Column("origin", TypeName = "enum")]
        public string? Origin { get; set; }

        [Column("Activo", TypeName = "int")]
        public int? Activo { get; set; }

        [Column("pro_rancer", TypeName = "varchar")]
        public string? ProRancer { get; set; }

        [Column("pro_inte", TypeName = "varchar")]
        public string? ProInte { get; set; }

        [Column("pro_comage", TypeName = "varchar")]
        public string? ProComage { get; set; }

        [Column("pro_monpag", TypeName = "decimal")]
        public decimal? ProMonpag { get; set; }

        [Column("idAgente", TypeName = "int")]
        public int? IdAgente { get; set; }

        [Column("cam_id", TypeName = "int")]
        public int? CamId { get; set; }

        [Column("fec_cap", TypeName = "datetime")]
        public DateTime FecCap { get; set; }

        [Column("pro_fecpag", TypeName = "datetime")]
        public DateTime? ProFecpag { get; set; }

        [Column("pro_cart", TypeName = "varchar")]
        public string? ProCart { get; set; }

        [Column("pro_idreg", TypeName = "varchar")]
        public string? ProIdreg { get; set; }

        [Column("jefcretie_id", TypeName = "int")]
        public int? JefcretieId { get; set; }

        [Column("Marca", TypeName = "varchar")]
        public string? Marca { get; set; }

        [Column("Modelo", TypeName = "varchar")]
        public string? Modelo { get; set; }

        [Column("Precio", TypeName = "float")]
        public float? Precio { get; set; }

        [Column("Medio_Capt", TypeName = "int")]
        public int? MedioCapt { get; set; }

        [Column("Fuente", TypeName = "varchar")]
        public string? Fuente { get; set; }

        [Column("Fecha_Capt", TypeName = "varchar")]
        public string? FechaCapt { get; set; }

        [Column("Vendedor", TypeName = "varchar")]
        public string? Vendedor { get; set; }

        [Column("Motivo_Rechazo", TypeName = "int")]
        public int? MotivoRechazo { get; set; }

        [Column("Termometro", TypeName = "int")]
        public int? Termometro { get; set; }

        [Column("pro_feccontac", TypeName = "datetime")]
        public DateTime? ProFeccontac { get; set; }

        [Column("pro_fecdesc", TypeName = "datetime")]
        public DateTime? ProFecdesc { get; set; }

        [Column("reasignado", TypeName = "bit")]
        public bool? Reasignado { get; set; }

        [Column("fec_estadoReasignado", TypeName = "datetime")]
        public DateTime? FecEstadoReasignado { get; set; }

        [Column("est_reasignado", TypeName = "int")]
        public int? EstReasignado { get; set; }

        [Column("pro_idAnterior", TypeName = "int")]
        public int? ProIdAnterior { get; set; }

        [Column("loader_id", TypeName = "int")]
        public int? LoaderId { get; set; }

        [Column("idconcesionario", TypeName = "int")]
        public int? Idconcesionario { get; set; }

        [Column("idgrupo", TypeName = "int")]
        public int? Idgrupo { get; set; }

        [Column("is_validate", TypeName = "tinyint")]
        public int IsValidate { get; set; }

        [Column("est_contacto_id", TypeName = "int")]
        public int? EstContactoId { get; set; }

        [Column("fec_est_contacto", TypeName = "datetime")]
        public DateTime? FecEstContacto { get; set; }

        [Column("ven_sec", TypeName = "varchar")]
        public string? VenSec { get; set; }

        [Column("referido", TypeName = "varchar")]
        public string? Referido { get; set; }

        [Column("genero", TypeName = "varchar")]
        public string? Genero { get; set; }


        public virtual MaestroProspecto? MaestroProspecto { get; set; }
    }
}
