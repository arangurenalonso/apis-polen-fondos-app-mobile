namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class LineasConfiguration : IEntityTypeConfiguration<Lineas>
    {
        public void Configure(EntityTypeBuilder<Lineas> builder)
        {
            builder.HasKey(c => c.NlinId);
        }
    }
}
