using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> builder)
        {
            // Mapping for table
            builder.ToTable("ProductInventory", "Production");

            // Set key for entity
            builder.HasKey(p => p.ProductInventoryID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ProductInventoryID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ProductInventoryID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
            builder.Property(p => p.WarehouseID).HasColumnType("varchar(5)").IsRequired();
            builder.Property(p => p.Quantity).HasColumnType("int").IsRequired();
            builder.Property(p => p.Stocks).HasColumnType("int").IsRequired();
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
                .HasOne(p => p.WarehouseFk)
                .WithMany(b => b.ProductInventories)
                .HasForeignKey(p => p.WarehouseID);
        }
    }
}
