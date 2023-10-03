namespace Infrastructure.Repositories.Persistence
{
    using Application.Contracts.Repositories;
    using Application.Exception;
    using Application.Helper;
    using Domain.Entities;
    using Infrastructure.Persistence;
    using Infrastructure.Repositories.Persistence.Common;
    using Microsoft.EntityFrameworkCore;

    public class VendedorRepository : RepositoryBase<Vendedores>, IVendedorRepository
    {
        private readonly ApplicationDbContext _context;

        public VendedorRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<Vendedores> ObtenerVendedorPorCodigo(string codVendedor)
        {
            var vendedor = await _context.Vendedores
                                          .Where(x => x.VenCod== codVendedor)
                                          .FirstOrDefaultAsync();
            if (vendedor == null)
            {
                throw new NotFoundException($"Vendedor con Id '{codVendedor}' no fue encontrado");
            }
            return vendedor;
        }
        public async Task<Vendedores> ObtenerVendedorAsignado(int idZona=0,string? codSupervisor="")
        {
            var idZonaAEvaluar = idZona;
            var codSupervisorAEvaluar = codSupervisor;
            if (!string.IsNullOrWhiteSpace(codSupervisor))
            {
                var supervisor = (await GetAsync(x => x.VenCod == codSupervisor)).FirstOrDefault();
                if (supervisor == null)
                {
                    throw new NotFoundException($"No se encontro el supervisor con Codigo {codSupervisor}");
                }
                if (supervisor.VenFcese!=null)
                {
                    idZonaAEvaluar = supervisor.ZonId;
                    codSupervisorAEvaluar = "";
                }
            }
            var vendedoresActivosDisponiblesPorZona = await GetAsync(
                                                                x =>
                                                                    x.VenCarCod == "003" &&
                                                                    (idZonaAEvaluar == 0 || x.ZonId == idZonaAEvaluar) &&
                                                                    (codSupervisorAEvaluar == null|| codSupervisorAEvaluar.Trim() == "" ||x.VenSupCod == codSupervisorAEvaluar) &&
                                                                    x.VenFcese == null &&
                                                                    x.Plead > 0//Los vendedores con Plead en 0
                                                                               //son vendedores que no se les puede asignar
                                                            );

            var vendedoresDisponiblesAsignar = vendedoresActivosDisponiblesPorZona
                                                                .Where(x => x.Qlead < x.Plead)
                                                                .ToList();
            if (!vendedoresDisponiblesAsignar.Any())
            {
                foreach (var vendedor in vendedoresActivosDisponiblesPorZona)
                {
                    vendedor.Qlead = 0;
                }
                UpdateRange(vendedoresActivosDisponiblesPorZona);
                await _context.SaveChangesAsync();
                vendedoresDisponiblesAsignar = vendedoresActivosDisponiblesPorZona;
            }
            var minPlead = vendedoresDisponiblesAsignar.Min(x => x.Plead) ?? 1;

            List<Vendedores> vendedoresDisponiblesAsginarConMinPlead;
            var comprobarAsignacionMinPleadNivelInferiorCompleta = vendedoresDisponiblesAsignar.Where(x => x.Qlead < minPlead - 1).ToList();
            if (comprobarAsignacionMinPleadNivelInferiorCompleta.Any())
            {
                vendedoresDisponiblesAsginarConMinPlead = comprobarAsignacionMinPleadNivelInferiorCompleta;
            }
            else
            {
                vendedoresDisponiblesAsginarConMinPlead = vendedoresDisponiblesAsignar.Where(x => x.Qlead < minPlead).ToList();
            }

            var vendedorAsignado = vendedoresDisponiblesAsginarConMinPlead.PickRandom();
            vendedorAsignado.Qlead = vendedorAsignado.Qlead + 1;

            await UpdateAsync(vendedorAsignado);
            return vendedorAsignado;

        }

    }
}
