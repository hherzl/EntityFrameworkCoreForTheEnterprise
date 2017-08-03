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
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "Production");

                entity.HasKey(p => p.ProductCategoryID);

                entity.Property(p => p.ProductCategoryID).UseSqlServerIdentityColumn();

                entity.Property(p => p.ProductCategoryName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                entity
                    .HasAlternateKey(p => new { p.ProductCategoryName })
                    .HasName("U_ProductCategoryName");
            });
        }
    }
}
