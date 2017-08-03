using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    [Export(typeof(IEntityMap))]
    public class ProductInventoryMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.ToTable("ProductInventory", "Production");

                entity.HasKey(p => p.ProductInventoryID);

                entity.Property(p => p.ProductInventoryID).UseSqlServerIdentityColumn();

                entity.Property(p => p.ProductID).HasColumnType("int").IsRequired();
                entity.Property(p => p.WarehouseID).HasColumnType("varchar(5)").IsRequired();
                entity.Property(p => p.Quantity).HasColumnType("int").IsRequired();
                entity.Property(p => p.Stocks).HasColumnType("int").IsRequired();
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                entity
                    .HasOne(p => p.ProductFk)
                    .WithMany(b => b.ProductInventories)
                    .HasForeignKey(p => p.ProductID)
                    .HasConstraintName("fk_ProductInventory_ProductID_Product");

                entity
                    .HasOne(p => p.WarehouseFk)
                    .WithMany(b => b.ProductInventories)
                    .HasForeignKey(p => p.WarehouseID)
                    .HasConstraintName("fk_ProductInventory_WarehouseID_Warehouse");
            });
        }
    }
}
