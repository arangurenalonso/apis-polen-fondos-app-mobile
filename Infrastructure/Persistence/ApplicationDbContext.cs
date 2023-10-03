namespace Infrastructure.Persistence
{
    using Application.Models.StoreProcedure.Response;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Programa> Programa { get; set; }
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<CertificadoGrupo> CertificadosGrupos { get; set; }
        public DbSet<Prospectos> Prospectos { get; set; }
        public DbSet<MaestroProspecto> MaestrosProspecto { get; set; }
        public DbSet<OrigenVentas> OrigenVentas { get; set; }
        public DbSet<Lineas> Lineas { get; set; }
        public DbSet<Medios> Medios { get; set; }        
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Zonas> Zonas { get; set; }
        public DbSet<LogFondos> LogFondos { get; set; }
        public DbSet<Vendedores> Vendedores { get; set; }

        public DbSet<RequestResponseLog> RequestResponseLog { get; set; }

        public DbSet<ComisionesParaLiquidacion> ComisionesParaLiquidacion { get; set; }
        public DbSet<DiscardReasonsEntity> DiscardReasonsEntity { get; set; }
        public DbSet<StatesContactosEntity> StatesContactosEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
