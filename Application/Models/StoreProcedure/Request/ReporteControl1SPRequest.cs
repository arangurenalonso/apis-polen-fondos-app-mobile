using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Models.StoreProcedure.Request
{
    public class ReporteControl1SPRequest
    { 
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }
        public string ven_gercod { get; set; }
        public string ven_gescod { get; set; } 
        public string ven_supcod { get; set; }
        public string ven_cod { get; set; }
        public string prioridad { get; set; }
        public string states { get; set; }
        public string medio { get; set; } 
    }
}
