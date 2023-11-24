namespace Domain.Enum.Dictionario
{
    public class EnumDictionaryProvider
    {
        public static Dictionary<TipoDocumentoEnum, string> TipoDocumentoDict = new Dictionary<TipoDocumentoEnum, string>()
        {
            {TipoDocumentoEnum.DNI, "DO02"},
            {TipoDocumentoEnum.RUC, "DO03"},
            {TipoDocumentoEnum.IMMIGRATION_CARD, "DO15"},
        };
        public static Dictionary<GeneroEnum, string> GeneroEnumDict = new Dictionary<GeneroEnum, string>()
        {
            {GeneroEnum.Masculino,"Masculino"}, 
            {GeneroEnum.Femenino, "Femenino"}
        };
        public static Dictionary<LineaEnum, string> LineaEnumDict = new Dictionary<LineaEnum, string>()
        {
            {LineaEnum.Vehiculo,"Vehiculo"},
            {LineaEnum.Inmueble,"Inmueble"}
        }; 

        public static Dictionary<CampaignOriginEnum, string> CampaignOriginEnumDict = new Dictionary<CampaignOriginEnum, string>()
        {
            { CampaignOriginEnum.Meta_Bogota_AutoNuevos, "BOGOTA_AUTO_META" }, 
            { CampaignOriginEnum.TikTok_Bogota_AutoNuevos, "BOGOTA_AUTO_TIKTOK" },
            { CampaignOriginEnum.Meta_Bogota_AutoUsados, "BOGOTA_SEMINUEVO_META" },
            { CampaignOriginEnum.TikTok_Bogota_AutoUsados, "BOGOTA_SEMINUEVO_TIKTOK" },
            { CampaignOriginEnum.FORMULARIO_HOME_WEB, "FORMULARIO_HOME_WEB" },

            

            { CampaignOriginEnum.Meta_Medellin_AutoNuevos, "MEDELLIN_AUTO_META" },
            { CampaignOriginEnum.TikTok_Medellin_AutoNuevos, "MEDELLIN_AUTO_TIKTOK" },
            { CampaignOriginEnum.Meta_Medellin_AutoUsados, "MEDELLIN_SEMINUEVO_META" },
            { CampaignOriginEnum.TikTok_Medellin_AutoUsados, "MEDELLIN_SEMINUEVO_TIKTOK" },

            { CampaignOriginEnum.Meta_Antioquia_AutoNuevos, "ANTIOQUIA_AUTO_META" },
            { CampaignOriginEnum.Meta_Antioquia_AutoUsados, "ANTIOQUIA_SEMINUEVO_META" }
        };
        public static Dictionary<ZonaEnum, string> ZonaEnumDict = new Dictionary<ZonaEnum, string>()
        {
            { ZonaEnum.Bogota, "BOGOTA" },
            { ZonaEnum.Medellin, "MEDELLIN" },
            { ZonaEnum.Antioquia, "ANTIOQUIA" },
        };
        public static Dictionary<TipoNegociacionEnum, string> TipoNegociacionEnumDict = new Dictionary<TipoNegociacionEnum, string>()
        {
            { TipoNegociacionEnum.VENTA_DIGITAL_B2C, "SALE" },
            { TipoNegociacionEnum.VENTA_PRESENCIAL_B2C, "COMPLEX" },
            { TipoNegociacionEnum.VENTA_CONTACT_CENTER_B2C, "GOODS" },
            { TipoNegociacionEnum.VENTA_ALIANZAS_B2B2C, "1" },
        };
    }
}