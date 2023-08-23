namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class OrigenVentasConfiguration : IEntityTypeConfiguration<OrigenVentas>
    {
        public void Configure(EntityTypeBuilder<OrigenVentas> builder)
        {
            builder.HasKey(c =>  c.Corivta);
        }
    }
}
