namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class StatesContactosEntityConfiguration : IEntityTypeConfiguration<StatesContactosEntity>
    {
        public void Configure(EntityTypeBuilder<StatesContactosEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
