namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class CertificadoGrupoConfiguration : IEntityTypeConfiguration<CertificadoGrupo>
    {
        public void Configure(EntityTypeBuilder<CertificadoGrupo> builder)
        {
            builder.HasKey(c => new { c.GrupoId, c.CertificadoId });
        }
    }
}
