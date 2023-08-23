namespace Application.Models.ConsumoApi.Bitrix24.Entities
{
    using System.Collections.Generic;
    public class UserBitrix24
    {
        public string XML_ID { get; set; }
        public bool ACTIVE { get; set; }
        public string NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public string IS_ONLINE { get; set; } = "Y";
        public string WORK_POSITION { get; set; }
        public List<int> UF_DEPARTMENT { get; set; } = new List<int>();
        public string USER_TYPE { get; set; }
    }
}
