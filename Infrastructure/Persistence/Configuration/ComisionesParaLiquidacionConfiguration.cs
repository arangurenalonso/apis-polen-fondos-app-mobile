namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class ComisionesParaLiquidacionConfiguration : IEntityTypeConfiguration<ComisionesParaLiquidacion>
    {
        public void Configure(EntityTypeBuilder<ComisionesParaLiquidacion> builder)
        {
            builder.HasKey(c => c.IdComision);
        }
    }
}
