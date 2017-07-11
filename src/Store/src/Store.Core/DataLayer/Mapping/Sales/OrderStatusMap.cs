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
            var entity = modelBuilder.Entity<OrderStatus>();

            entity.ToTable("OrderStatus", "Sales");

            entity.HasKey(p => p.OrderStatusID);

            entity.Property(p => p.Description).HasColumnType("varchar(100)");

            entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");

            entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
