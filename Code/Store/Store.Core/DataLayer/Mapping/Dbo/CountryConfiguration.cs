using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Mapping.Dbo
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Mapping for table
            builder.ToTable("Country", "dbo");

            // Set key for entity
            builder.HasKey(p => p.CountryID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.CountryID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
