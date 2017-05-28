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
            var entity = modelBuilder.Entity<ProductInventory>();

            entity.ToTable("ProductInventory", "Production");

            entity.HasKey(p => p.ProductInventoryID);

            entity.Property(p => p.ProductInventoryID).UseSqlServerIdentityColumn();

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
