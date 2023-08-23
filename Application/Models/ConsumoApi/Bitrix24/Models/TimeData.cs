namespace Application.Models.ConsumoApi.Bitrix24.Models
{
    using System;
    public class TimeData
    {
        public double Start { get; set; }
        public double Finish { get; set; }
        public double Duration { get; set; }
        public double Processing { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public double OperatingResetAt { get; set; }
        public double Operating { get; set; }
    }
}
