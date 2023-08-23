namespace Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    [Table("link_comisionesparaliquidacion")]
    public class ComisionesParaLiquidacion
    {
        [Key]
        [Column("idComision", TypeName = "int")]
        public int IdComision { get; set; }

        [Column("idGrupo", TypeName = "int")]
        public int? IdGrupo { get; set; }

        [Column("pro_id", TypeName = "int")]
        public int? ProId { get; set; }

        [Column("codigo", TypeName = "varchar")]
        public string Codigo { get; set; }

        [Column("razonSocial", TypeName = "varchar")]
        public string? RazonSocial { get; set; }

        [Column("ruc", TypeName = "varchar")]
        public string? Ruc { get; set; }

        [Column("tipoLiquidacion", TypeName = "varchar")]
        public string? TipoLiquidacion { get; set; }

        [Column("tipoComision", TypeName = "varchar")]
        public string? TipoComision { get; set; }

        [Column("moneda", TypeName = "varchar")]
        public string? Moneda { get; set; }

        [Column("montoComision", TypeName = "decimal")]
        public decimal? MontoComision { get; set; }

        [Column("montoDescuento", TypeName = "decimal")]
        public decimal? MontoDescuento { get; set; }

        [Column("montoTotal", TypeName = "decimal")]
        public decimal? MontoTotal { get; set; }

        [Column("comisionVendedores", TypeName = "tinyint")]
        public int? ComisionVendedores { get; set; }

        [Column("liquidado", TypeName = "tinyint")]
        public int? Liquidado { get; set; }

        [Column("liquidadoVendedor", TypeName = "tinyint")]
        public int? LiquidadoVendedor { get; set; }

        [Column("idLiquidacionesVendedor", TypeName = "int")]
        public int? IdLiquidacionesVendedor { get; set; }

        [Column("idLiquidaciones", TypeName = "int")]
        public int? IdLiquidaciones { get; set; }

        [Column("mesVenta", TypeName = "datetime")]
        public DateTime? MesVenta { get; set; }

        [Column("ci_pact", TypeName = "decimal")]
        public decimal? CiPact { get; set; }

        [Column("ci_pact_sigv", TypeName = "decimal")]
        public decimal? CiPactSigv { get; set; }

        [Column("ci_pag", TypeName = "decimal")]
        public decimal? CiPag { get; set; }

        [Column("ci_pag_sigv", TypeName = "decimal")]
        public decimal? CiPagSigv { get; set; }

        [Column("CC", TypeName = "decimal")]
        public decimal? CC { get; set; }

        [Column("porc_comision", TypeName = "decimal")]
        public decimal? PorcComision { get; set; }

        [Column("sin_igv", TypeName = "decimal")]
        public decimal? SinIgv { get; set; }

        [Column("idLiquidacionesTrabajador", TypeName = "int")]
        public int? IdLiquidacionesTrabajador { get; set; }

        [Column("foja", TypeName = "int")]
        public int Foja { get; set; }

        [Column("centro_costo", TypeName = "varchar")]
        public string? CentroCosto { get; set; }

        [Column("fecha_pago", TypeName = "datetime")]
        public DateTime? FechaPago { get; set; }
    }
}
