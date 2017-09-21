using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class OrderStatusMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                // Mapping for table
                entity.ToTable("OrderStatus", "Sales");

                // Set key for entity
                entity.HasKey(p => p.OrderStatusID);

                // Set mapping for columns
                entity.Property(p => p.Description).HasColumnType("varchar(100)");
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                // Set concurrency token for entity
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            });
        }
    }
}
