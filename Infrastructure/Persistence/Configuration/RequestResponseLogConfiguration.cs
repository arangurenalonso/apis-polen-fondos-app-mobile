namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    internal class RequestResponseLogConfiguration : IEntityTypeConfiguration<RequestResponseLog>
    {
        public void Configure(EntityTypeBuilder<RequestResponseLog> builder)
        {
            builder.ToTable("RequestResponseLog");

            builder.Property(p => p.Id).IsRequired();

            builder.HasKey(p => p.Id);
        }
    }
}
