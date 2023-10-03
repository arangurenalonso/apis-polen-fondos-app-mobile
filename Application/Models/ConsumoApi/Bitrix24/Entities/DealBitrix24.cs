namespace Application.Models.ConsumoApi.Bitrix24.Entities
{
    public class DealBitrix24
    { 
        public int? ID { get; set; }
        public string? TITLE { get; set; } 
        public string? TYPE_ID { get; set; }
        public string? STAGE_ID { get; set; } 
        public string? COMPANY_ID { get; set; }
        public string? CONTACT_ID { get; set; }
        public string? OPENED { get; set; }
        public string? ASSIGNED_BY_ID { get; set; }
        public int? PROBABILITY { get; set; }
        public string? CURRENCY_ID { get; set; }
        public double? OPPORTUNITY { get; set; }
        public string? SOURCE_ID { get; set; }//Origen de venta
        public string? CATEGORY_ID { get; set; }//Categoria Tipo Negociacion 
        public string UF_CRM_1695762208 { get; set; } = "";
        public string UF_CRM_1695762237 { get; set; } = "";
        public string UF_CRM_1695762254 { get; set; } = "";

    }
}
