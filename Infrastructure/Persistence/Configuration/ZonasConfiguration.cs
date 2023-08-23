namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class ZonasConfiguration : IEntityTypeConfiguration<Zonas>
    {
        public void Configure(EntityTypeBuilder<Zonas> builder)
        {
            builder.HasKey(x => x.ZonId);
        }
    }
}
