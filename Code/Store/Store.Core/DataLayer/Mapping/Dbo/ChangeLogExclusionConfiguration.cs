using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Mapping.Dbo
{
    public class ChangeLogExclusionConfiguration : IEntityTypeConfiguration<ChangeLogExclusion>
    {
        public void Configure(EntityTypeBuilder<ChangeLogExclusion> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLogExclusion", "dbo");

            // Set key for entity
            builder.HasKey(p => p.ChangeLogExclusionID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ChangeLogExclusionID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.EntityName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
        }
    }
}
