using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            // Mapping for table
            builder.ToTable("Warehouse", "Production");

            // Set key for entity
            builder.HasKey(p => p.WarehouseID);

            // Set mapping for columns
            builder.Property(p => p.WarehouseID).HasColumnType("varchar(5)").IsRequired();
            builder.Property(p => p.WarehouseName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
