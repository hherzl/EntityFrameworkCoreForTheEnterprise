using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class PaymentMethodMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                // Mapping for table
                entity.ToTable("PaymentMethod", "Sales");

                // Set key for entity
                entity.HasKey(p => p.PaymentMethodID);

                // Set mapping for columns
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
