using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            // Mapping for table
            builder.ToTable("ProductCategory", "Production");

            // Set key for entity
            builder.HasKey(p => p.ProductCategoryID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ProductCategoryID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ProductCategoryID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ProductCategoryName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for uniques
            builder
                .HasAlternateKey(p => new { p.ProductCategoryName })
                .HasName("U_ProductCategoryName");
        }
    }
}
