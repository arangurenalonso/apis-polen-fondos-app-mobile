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
    }
}
