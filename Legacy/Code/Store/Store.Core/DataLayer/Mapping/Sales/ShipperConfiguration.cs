using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            // Mapping for table
            builder.ToTable("Shipper", "Sales");

            // Set key for entity
            builder.HasKey(p => p.ShipperID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ShipperID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.CompanyName).HasColumnType("varchar(100)");
            builder.Property(p => p.ContactName).HasColumnType("varchar(100)");
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for uniques
            builder
                .HasAlternateKey(p => new { p.CompanyName })
                .HasName("U_CompanyName");
        }
    }
}
