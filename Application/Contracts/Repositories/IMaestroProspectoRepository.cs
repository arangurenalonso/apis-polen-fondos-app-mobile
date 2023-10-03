namespace Application.Contracts.Repositories
{
    using Application.Contracts.Repositories.Base;
    using Domain.Entities;
    public interface IMaestroProspectoRepository : IRepositoryBase<MaestroProspecto>
    {
        Task<MaestroProspecto> ObtenerMaestroProspectoPorId(int idMaestroProspecto);
        Task<int> EstablerDatosMinimoYRegistrarMaestroProspecto(MaestroProspecto maestroProspecto, string idContactBitrix24);
        Task<MaestroProspecto?> VerificarRegistroPrevioMaestroProspecto(string numCelular);
    }
}
