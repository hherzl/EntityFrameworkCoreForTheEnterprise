using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.Core.Domain.Configurations.Warehouse
{
    public class ProductUnitPriceHistoryConfiguration : IEntityTypeConfiguration<ProductUnitPriceHistory>
    {
        public void Configure(EntityTypeBuilder<ProductUnitPriceHistory> builder)
        {
            // Mapping for table
            builder.ToTable("ProductUnitPriceHistory", "Warehouse");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ProductID).IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("decimal(8, 4)").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
