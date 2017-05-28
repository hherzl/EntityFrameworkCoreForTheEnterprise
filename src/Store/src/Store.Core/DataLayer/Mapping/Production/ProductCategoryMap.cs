using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    [Export(typeof(IEntityMap))]
    public class ProductCategoryMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ProductCategory>();

            entity.ToTable("ProductCategory", "Production");

            entity.HasKey(p => p.ProductCategoryID);

            entity.Property(p => p.ProductCategoryID).UseSqlServerIdentityColumn();

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
