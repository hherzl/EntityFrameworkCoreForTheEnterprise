using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Dbo;

namespace OnlineStore.Core.Domain.Configurations.Dbo
{
    public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLog", "dbo");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ClassName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.OriginalValue).HasColumnType("varchar(max)");
            builder.Property(p => p.CurrentValue).HasColumnType("varchar(max)");
            builder.Property(p => p.UserName).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.ChangeDate).HasColumnType("varchar(128)").IsRequired();
        }
    }
}
