namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class StateConfiguration : IEntityTypeConfiguration<StateEntity>
    {
        public void Configure(EntityTypeBuilder<StateEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
