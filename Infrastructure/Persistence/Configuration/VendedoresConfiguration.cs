namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class VendedoresConfiguration : IEntityTypeConfiguration<Vendedores>
    {
        public void Configure(EntityTypeBuilder<Vendedores> builder)
        {
            builder.HasKey(x =>x.VenCod);
        }
    }
}
