namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class ProspectoConfiguration : IEntityTypeConfiguration<Prospectos>
    {
        public void Configure(EntityTypeBuilder<Prospectos> builder)
        {
            builder.HasKey(x => x.ProId);
        }
    }
}
