namespace Infrastructure.Persistence.Configuration
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    public class LogFondosConfiguration : IEntityTypeConfiguration<LogFondos>
    {
        public void Configure(EntityTypeBuilder<LogFondos> builder)
        {
            builder.HasKey(x => x.Logid);
        }
    }
}
