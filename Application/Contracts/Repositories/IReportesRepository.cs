namespace Application.Contracts.Repositories
{
    using Application.Models.StoreProcedure.Request;
    using Application.Models.StoreProcedure.Response;
    public interface IReportesRepository
    {
        Task<List<ReporteControl1SPResponse>> ObtenerReporteControl1(ReporteControl1SPRequest reporteControl1SPRequest);
    }
}
