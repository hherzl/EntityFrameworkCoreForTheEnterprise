using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.DomainDrivenDesign.Dbo;

namespace OnlineStore.Core.DomainDrivenDesign.Configurations.Dbo
{
    public class ChangeLogExclusionConfiguration : IEntityTypeConfiguration<ChangeLogExclusion>
    {
        public void Configure(EntityTypeBuilder<ChangeLogExclusion> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLogExclusion", "dbo");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.EntityName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
        }
    }
}
