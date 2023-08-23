using Application.Models.ConsumoApi.Bitrix24.Models;

namespace Application.Models.ConsumoApi.Bitrix24.Entities
{
    public class ContactBitrix24
    {
        public string? NAME { get; set; } 
        public string? SECOND_NAME { get; set; }
        public string? LAST_NAME { get; set; }
        public string? OPENED { get; set; } = "Y";
        public int? ASSIGNED_BY_ID { get; set; } = 26;
        public string? TYPE_ID { get; set; } = "CLIENT";
        public string? ADDRESS { get; set; }
        public string? SOURCE_ID { get; set; } = "SELF";
        public string? UF_CRM_1597092493 { get; set; } = "Cédula de Ciudadanía";//Tipo de documento
        public string? UF_CRM_1594982869 { get; set; }//Numero de documento

        public List<TypedField> PHONE { get; set; } = new List<TypedField>();
        public List<TypedField> EMAIL { get; set; } = new List<TypedField>();

    }
}
