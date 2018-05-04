using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Mapping.Dbo
{
    public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLog", "dbo");

            // Set key for entity
            builder.HasKey(p => p.ChangeLogID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ChangeLogID).UseSqlServerIdentityColumn();

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
