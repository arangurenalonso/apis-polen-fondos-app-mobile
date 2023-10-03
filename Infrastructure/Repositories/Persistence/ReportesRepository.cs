namespace Infrastructure.Repositories.Persistence
{
    using Application.Contracts.Repositories;
    using Application.Models.StoreProcedure.Request;
    using Application.Models.StoreProcedure.Response;
    using Infrastructure.Persistence;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using MySqlConnector;
    using System.Data;

    public class ReportesRepository : IReportesRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportesRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<List<ReporteControl1SPResponse>> ObtenerReporteControl1(ReporteControl1SPRequest reporteControl1SPRequest)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_get_ReporteControl1";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@inicio", reporteControl1SPRequest.start_at));
                    command.Parameters.Add(new MySqlParameter("@fin", reporteControl1SPRequest.end_at));
                    command.Parameters.Add(new MySqlParameter("@codger", reporteControl1SPRequest.ven_gercod));
                    command.Parameters.Add(new MySqlParameter("@codsubger", reporteControl1SPRequest.ven_gescod));
                    command.Parameters.Add(new MySqlParameter("@codsup", reporteControl1SPRequest.ven_supcod));
                    command.Parameters.Add(new MySqlParameter("@codven", reporteControl1SPRequest.ven_cod));
                    command.Parameters.Add(new MySqlParameter("@prioridad", reporteControl1SPRequest.prioridad));
                    command.Parameters.Add(new MySqlParameter("@estado", reporteControl1SPRequest.states));
                    command.Parameters.Add(new MySqlParameter("@medio", reporteControl1SPRequest.medio.ToUpper() == "ALL" ? 0 : reporteControl1SPRequest.medio));

                    _context.Database.OpenConnection();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        var reporteControlList = new List<ReporteControl1SPResponse>();

                        while (result.Read())
                        {
                            var reporteControl = new ReporteControl1SPResponse
                            {
                                Vendedor = result["Vendedor"] != DBNull.Value ? result["Vendedor"].ToString() : null,
                                Fecha_Registro = result["Fecha_Registro"] != DBNull.Value ? Convert.ToDateTime(result["Fecha_Registro"]) : (DateTime?)null,
                                Prospecto = result["Prospecto"] != DBNull.Value ? result["Prospecto"].ToString() : null,
                                Estado_Actual = result["Estado_Actual"] != DBNull.Value ? result["Estado_Actual"].ToString() : null,
                                Dias = result["Dias"] != DBNull.Value ? Convert.ToInt32(result["Dias"]) : 0,
                                Fecha_Capt = result["Fecha_Capt"] != DBNull.Value ? Convert.ToDateTime(result["Fecha_Capt"]) : (DateTime?)null,
                                DiasCaptacion = result["DiasCaptacion"] != DBNull.Value ? Convert.ToInt32(result["DiasCaptacion"]) : (int?)null,
                                Correo = result["Correo"] != DBNull.Value ? result["Correo"].ToString() : null,
                                Zona = result["Zona"] != DBNull.Value ? result["Zona"].ToString() : null,
                                Distrito = result["Distrito"] != DBNull.Value ? result["Distrito"].ToString() : null,
                                Producto = result["Producto"] != DBNull.Value ? result["Producto"].ToString() : null,
                                Certificado = result["Certificado"] != DBNull.Value ? result["Certificado"].ToString() : null,
                                Origen_Venta = result["Origen_Venta"] != DBNull.Value ? result["Origen_Venta"].ToString() : null,
                                Medio = result["Medio"] != DBNull.Value ? result["Medio"].ToString() : null,
                                Descartado = result["descartado"] != DBNull.Value ? result["descartado"].ToString() : null,
                                Prioridad = result["prioridad"] != DBNull.Value ? result["prioridad"].ToString() : null,
                                Comentario = result["Comentario"] != DBNull.Value ? result["Comentario"].ToString() : null,
                                Celular = result["Celular"] != DBNull.Value ? result["Celular"].ToString() : null,
                                Supervisor = result["Supervisor"] != DBNull.Value ? result["Supervisor"].ToString() : null,
                                MotivoDescarte = result["MotivoDescarte"] != DBNull.Value ? result["MotivoDescarte"].ToString() : null,
                                FechaInscripcion = result["FechaInscripcion"] != DBNull.Value  ? Convert.ToDateTime(result["FechaInscripcion"]) : (DateTime?)null
                            };

                            reporteControlList.Add(reporteControl);
                        }

                        return reporteControlList;
                    }

                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            finally
            {
                if (_context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    _context.Database.CloseConnection();
                }
            }
        }

    }
}
