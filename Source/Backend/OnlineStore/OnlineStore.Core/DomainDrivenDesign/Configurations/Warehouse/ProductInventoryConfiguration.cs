using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.Domain.Configurations.Warehouse
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> builder)
        {
            // Mapping for table
            builder.ToTable("ProductInventory", "Warehouse");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
            builder.Property(p => p.LocationID).HasColumnType("varchar(5)").IsRequired();
            builder.Property(p => p.OrderDetailID).HasColumnType("bigint");
            builder.Property(p => p.Quantity).HasColumnType("int").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(p => p.ProductFk)
                .WithMany(b => b.ProductInventories)
                .HasForeignKey(p => p.ProductID);

            builder
                .HasOne(p => p.LocationFk)
                .WithMany(b => b.ProductInventories)
                .HasForeignKey(p => p.LocationID);
        }
    }
}
