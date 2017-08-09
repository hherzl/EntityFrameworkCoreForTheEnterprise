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
                // Mapping for table
                entity.ToTable("ProductCategory", "Production");

                // Set key for entity
                entity.HasKey(p => p.ProductCategoryID);

                // Set identity for entity (auto increment)
                entity.Property(p => p.ProductCategoryID).UseSqlServerIdentityColumn();

                // Set mapping for columns
                entity.Property(p => p.ProductCategoryID).HasColumnType("int").IsRequired();
                entity.Property(p => p.ProductCategoryName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                // Set concurrency token for entity
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                // Add configuration for uniques
                entity
                    .HasAlternateKey(p => new { p.ProductCategoryName })
                    .HasName("U_ProductCategoryName");
            });
        }
    }
}
