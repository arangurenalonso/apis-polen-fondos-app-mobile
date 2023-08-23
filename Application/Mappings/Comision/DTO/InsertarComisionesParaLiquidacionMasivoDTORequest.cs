namespace Application.Mappings.Comision.DTO
{
    public class InsertarComisionesParaLiquidacionMasivoDTORequest
    {
        public int? IdGrupo { get; set; }

        public int? ProId { get; set; }

        public string? Codigo { get; set; } = "";

        public string? RazonSocial { get; set; }

        public string? Ruc { get; set; }
        public string? TipoLiquidacion { get; set; }

        public string? TipoComision { get; set; }

        public string? Moneda { get; set; }

        public decimal? MontoComision { get; set; }

        public decimal? MontoDescuento { get; set; }
        public decimal? MontoTotal { get; set; }
        public int? ComisionVendedores { get; set; }

        public int? Liquidado { get; set; }

        public int? LiquidadoVendedor { get; set; }

        public int? IdLiquidacionesVendedor { get; set; }

        public int? IdLiquidaciones { get; set; }

        public DateTime? MesVenta { get; set; }

        public decimal? CiPact { get; set; }

        public decimal? CiPactSigv { get; set; }
        public decimal? CiPag { get; set; }

        public decimal? CiPagSigv { get; set; }
        public decimal? CC { get; set; }

        public decimal? PorcComision { get; set; }
        public decimal? SinIgv { get; set; }

        public int? IdLiquidacionesTrabajador { get; set; }
        public int? Foja { get; set; }

        public string? CentroCosto { get; set; }

        public DateTime? FechaPago { get; set; }
    }
}
