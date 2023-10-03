namespace Infrastructure.Repositories.Persistence
{
    using Application.Contracts.Repositories;
    using Application.Exception;
    using Application.Helper;
    using Domain.Entities;
    using Infrastructure.Persistence;
    using Infrastructure.Repositories.Persistence.Common;
    using Microsoft.EntityFrameworkCore;

    public class MaestroProspectoRepository : RepositoryBase<MaestroProspecto>, IMaestroProspectoRepository
    {
        private readonly ApplicationDbContext _context;

        public MaestroProspectoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<MaestroProspecto> ObtenerMaestroProspectoPorId(int idMaestroProspecto)
        {
            var maestroProspecto = await _context.MaestrosProspecto
                                            .Where(x => x.MaeId == idMaestroProspecto)
                                            .FirstOrDefaultAsync();
            if (maestroProspecto == null)
            {
                throw new NotFoundException($"Maestro Prospecto con Id '{idMaestroProspecto}' no fue encontrado");
            }
            return maestroProspecto;
        }
        public async Task<int> EstablerDatosMinimoYRegistrarMaestroProspecto(MaestroProspecto maestroProspecto, string idContactBitrix24)
        {
            var fechaActual = DateTime.Now;
            maestroProspecto.MaeFeccrea = fechaActual;
            maestroProspecto.MaeFecactu = fechaActual;
            maestroProspecto.DocId = "DO01";
            maestroProspecto.BitrixID = idContactBitrix24;
            var entityAdded = await AddAsync(maestroProspecto);
            return entityAdded.MaeId;
        }
        public async Task<MaestroProspecto?> VerificarRegistroPrevioMaestroProspecto(string numCelular)
        {
            var maestroProspecto = (await GetAsync(x => x.MaeCel1 == MethodHelper.GetLastNCharacters(numCelular, 10) )).FirstOrDefault();
            return maestroProspecto;
        }
    }
}
