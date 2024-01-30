namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class PuntoDeVentaConfiguration : IEntityTypeConfiguration<PuntoDeVenta>
    {
        public void Configure(EntityTypeBuilder<PuntoDeVenta> builder)
        {
            builder.HasKey(x => x.PvtaCod);
        }
    }
}
