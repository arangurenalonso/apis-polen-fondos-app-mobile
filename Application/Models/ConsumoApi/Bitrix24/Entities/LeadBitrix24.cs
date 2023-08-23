using Application.Models.ConsumoApi.Bitrix24.Models;

namespace Application.Models.ConsumoApi.Bitrix24.Entities
{
    public class LeadBitrix24
    {
        public string TITLE { get; set; }
        public string NAME { get; set; }
        public string SECOND_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string STATUS_ID { get; set; } = "NEW";
        public string OPENED { get; set; } = "Y";
        public string ASSIGNED_BY_ID { get; set; } = "26";
        public string CURRENCY_ID { get; set; } = "USD";
        public decimal OPPORTUNITY { get; set; } = default;
        public string CONTACT_ID { get; set; }
        public List<TypedField> PHONE { get; set; } = new List<TypedField>();
        public List<TypedField> WEB { get; set; } = new List<TypedField>();
    }
}
