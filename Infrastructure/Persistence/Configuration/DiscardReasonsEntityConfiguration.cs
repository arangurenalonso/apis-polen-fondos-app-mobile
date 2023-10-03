namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class DiscardReasonsEntityConfiguration : IEntityTypeConfiguration<DiscardReasonsEntity>
    {
        public void Configure(EntityTypeBuilder<DiscardReasonsEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
