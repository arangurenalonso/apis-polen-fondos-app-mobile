namespace Infrastructure.Repositories.Persistence
{
    using Application.Contracts.Repositories;
    using Application.Mappings.Prospecto.DTO;
    using Domain.Entities;
    using Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using MySqlConnector;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using Infrastructure.Repositories.Persistence.Common;
    using Application.Contracts.Repositories.Base;
    using Domain.Enum;
    using Application.Exception;

    public class ProspectoRepository : RepositoryBase<Prospectos>, IProspectoRepository
    {
        public ProspectoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<SPGetDatosProspectoVendResponse?> ObtenerDatosProspectoPorVendedor(string CodigoVendedor, string nroDocumento)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_get_datosprospecto_vend";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@codven", CodigoVendedor));
                    command.Parameters.Add(new MySqlParameter("@nrodoc", nroDocumento));

                    _context.Database.OpenConnection();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        var datosProspecto = new List<SPGetDatosProspectoVendResponse>();

                        while (result.Read())
                        {
                            var prospecto = new SPGetDatosProspectoVendResponse();

                            if (result["Codigo"] != DBNull.Value && int.TryParse(result["Codigo"].ToString(), out var codigo))
                            {
                                prospecto.Codigo = codigo;
                            }

                            prospecto.Nombres = result["Nombres"] != DBNull.Value ? result["Nombres"].ToString() : null;
                            prospecto.Grupo = result["Grupo"] != DBNull.Value ? result["Grupo"].ToString() : null;

                            if (result["Certificado"] != DBNull.Value && decimal.TryParse(result["Certificado"].ToString(), out var certificado))
                            {
                                prospecto.Certificado = certificado;
                            }

                            datosProspecto.Add(prospecto);
                        }

                        return datosProspecto.FirstOrDefault();
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

        public async Task<SpGetListaDatosFojaResult?> ObtenerDatosFoja(int proid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_get_lista_datos_foja";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@proid", proid));

                    _context.Database.OpenConnection();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        var fojaDataList = new List<SpGetListaDatosFojaResult>();

                        while (result.Read())
                        {
                            var fojaData = new SpGetListaDatosFojaResult
                            {
                                TipoDoc = result["tipo_doc"] != DBNull.Value ? result["tipo_doc"].ToString() : string.Empty,
                                DocId = result["doc_id"] != DBNull.Value ? result["doc_id"].ToString() : string.Empty,
                                Nombres = result["nombres"] != DBNull.Value ? result["nombres"].ToString() : string.Empty,
                                Paterno = result["paterno"] != DBNull.Value ? result["paterno"].ToString() : string.Empty,
                                Materno = result["materno"] != DBNull.Value ? result["materno"].ToString() : string.Empty,
                                RazSoc = result["raz_soc"] != DBNull.Value ? result["raz_soc"].ToString() : string.Empty,
                                Celular = result["celular"] != DBNull.Value ? result["celular"].ToString() : string.Empty,
                                Email = result["email"] != DBNull.Value ? result["email"].ToString() : string.Empty,
                                OvtaCod = result["ovta_cod"] != DBNull.Value ? result["ovta_cod"].ToString() : string.Empty,
                                PvtaCod = result["pvta_cod"] != DBNull.Value ? result["pvta_cod"].ToString() : string.Empty,
                                TieCod = result["tie_cod"] != DBNull.Value ? result["tie_cod"].ToString() : string.Empty,
                                VentieCod = result["ventie_cod"] != DBNull.Value ? result["ventie_cod"].ToString() : string.Empty,
                                VenCod = result["ven_cod"] != DBNull.Value ? result["ven_cod"].ToString() : string.Empty,
                                VenSupcod = result["ven_supcod"] != DBNull.Value ? result["ven_supcod"].ToString() : string.Empty,
                                VenGescod = result["ven_gescod"] != DBNull.Value ? result["ven_gescod"].ToString() : string.Empty,
                                VenGercod = result["ven_gercod"] != DBNull.Value ? result["ven_gercod"].ToString() : string.Empty,
                                GruId = result["gru_id"] != DBNull.Value ? result["gru_id"].ToString() : string.Empty,
                                CerId = result["cer_id"] != DBNull.Value ? result["cer_id"].ToString() : string.Empty,

                            };
                            if (result["tipo_per"] != DBNull.Value && int.TryParse(result["tipo_per"].ToString(), out var tipoPer))
                            {
                                fojaData.TipoPer = tipoPer;
                            }

                            if (result["lin_id"] != DBNull.Value && int.TryParse(result["lin_id"].ToString(), out var linId))
                            {
                                fojaData.LinId = linId;
                            }
                            if (result["tipo_adj"] != DBNull.Value && int.TryParse(result["tipo_adj"].ToString(), out var tipoAdj))
                            {
                                fojaData.TipoAdj = tipoAdj;
                            }

                            if (result["PorcCINS"] != DBNull.Value && decimal.TryParse(result["PorcCINS"].ToString(), out var porcCINS))
                            {
                                fojaData.PorcCINS = porcCINS;
                            }


                            fojaDataList.Add(fojaData);
                        }

                        return fojaDataList.FirstOrDefault();
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


        public async Task<Prospectos> ObtenerProspectoPorId(int idProspecto)
        {
            var prospecto = await _context.Prospectos
                                            .Where(x => x.ProId == idProspecto)
                                            .FirstOrDefaultAsync();
            if(prospecto == null)
            {
                throw new NotFoundException($"Prospecto con Id '{idProspecto}' no fue encontrado");
            }
            return prospecto;
        }
        public async Task<Prospectos?> ObtenerUltimoProspectoPorIdMaestroProspecto(int idMaestroProspecto)
        {
            var prospecto = await _context.Prospectos
                                           .Where(x => x.MaeId == idMaestroProspecto)
                                          .OrderByDescending(p => p.ProFecpro)
                                          .FirstOrDefaultAsync();
            return prospecto;
        }
        public async Task<(bool,Prospectos? prospecto)> VerificarIngresoProspecto(int idMaestroProspecto) 
        {
            var prospecto = await _context.Prospectos
                                            .Where(x=>x.MaeId== idMaestroProspecto)
                                           .OrderByDescending(p => p.ProFecpro)
                                           .FirstOrDefaultAsync();
            if (prospecto==null)
            {
                return (true,null);
            }
            DateTime fechaProspecto = prospecto.ProFecpro;
            DateTime fechaActual = DateTime.Now;
            int diasDiferencia = (fechaActual - fechaProspecto).Days;
            return (diasDiferencia >= 30, prospecto);
        }
        public async Task<int> EstablecerDatosMinimosYRegistrarProspecto(Prospectos prospecto, 
            int idMaestroProspecto, string idDealBitrix24, Vendedores vendedor, int zonaId,string idOrigen)
        {
            var fechaActual=DateTime.Now; 
            prospecto.BitrixID = idDealBitrix24;
            prospecto.MaeId = idMaestroProspecto;
            prospecto.EstId = (int)EstadoEnum.NoContactado;
            prospecto.ProFecpro = fechaActual;
            prospecto.ProFecest = fechaActual;
            prospecto.ProFecasi = fechaActual;
            prospecto.FecCap = fechaActual;
            prospecto.TipoPersona = 1;
            prospecto.Origin = "BULK_LOAD";
            prospecto.IsValidate = 0;
            prospecto.NlinId = 1;
            prospecto.Corivta = idOrigen;
            prospecto.ZonId = zonaId;
            prospecto.VenCod = vendedor.VenCod;
            prospecto.VenSupcod = vendedor.VenSupCod;
            prospecto.VenGescod = vendedor.VenGesCod;
            prospecto.VenGercod = vendedor.VenGerCod;

            if (idOrigen== "OV02")
            {
                prospecto.IntId = true;
            }

            var entityAdded = await AddAsync(prospecto);
            return entityAdded.ProId;
        }

    }
}
