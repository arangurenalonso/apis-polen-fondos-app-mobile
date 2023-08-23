namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class MaestroProspectoConfiguration : IEntityTypeConfiguration<MaestroProspecto>
    {
        public void Configure(EntityTypeBuilder<MaestroProspecto> builder)
        {
            builder.HasKey(x => x.MaeId);


            builder
                .HasMany(maestroProspecto => maestroProspecto.Prospectos)
                .WithOne(prospecto => prospecto.MaestroProspecto)
                .HasForeignKey(prospecto => prospecto.MaeId);
        }
    }
}
