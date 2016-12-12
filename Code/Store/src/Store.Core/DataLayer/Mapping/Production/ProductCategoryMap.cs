using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    public class ProductCategoryMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ProductCategory>();

            entity.ToTable("ProductCategory", "Production");

            entity.HasKey(p => p.ProductCategoryID);

            entity.Property(p => p.ProductCategoryID).UseSqlServerIdentityColumn();
        }
    }
}
