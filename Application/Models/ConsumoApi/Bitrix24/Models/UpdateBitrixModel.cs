namespace Application.Models.ConsumoApi.Bitrix24.Models
{
    public class UpdateBitrixModel<T>
    {
        public string id { get; set; }
        public T fields { get; set; }
    }
}
