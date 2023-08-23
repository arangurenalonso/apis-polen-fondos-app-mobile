namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class MediosConfiguration : IEntityTypeConfiguration<Medios>
    {
        public void Configure(EntityTypeBuilder<Medios> builder)
        {
            builder.HasKey(x=>x.MedId);
        }
    }
}
