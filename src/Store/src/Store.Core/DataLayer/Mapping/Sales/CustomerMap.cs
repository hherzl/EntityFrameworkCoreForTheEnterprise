using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class CustomerMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                // Mapping for table
                entity.ToTable("Customer", "Sales");

                // Set key for entity
                entity.HasKey(p => p.CustomerID);

                // Set identity for entity (auto increment)
                entity.Property(p => p.CustomerID).UseSqlServerIdentityColumn();

                // Set mapping for columns
                entity.Property(p => p.CompanyName).HasColumnType("varchar(100)");
                entity.Property(p => p.ContactName).HasColumnType("varchar(100)");
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                // Set concurrency token for entity
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                // Add configuration for uniques
                entity
                    .HasAlternateKey(p => new { p.CompanyName })
                    .HasName("U_CompanyName");
            });
        }
    }
}
