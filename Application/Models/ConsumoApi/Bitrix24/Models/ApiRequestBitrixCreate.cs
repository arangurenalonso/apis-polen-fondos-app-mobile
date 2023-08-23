namespace Application.Models.ConsumoApi.Bitrix24.Models
{
    using Newtonsoft.Json;
    public class ApiRequestBitrixCreate<T>
    {
        public T fields { get; set; }
        [JsonProperty("params")]
        public ParamsApiRequestCreate Params { get; set; }
    }
}
