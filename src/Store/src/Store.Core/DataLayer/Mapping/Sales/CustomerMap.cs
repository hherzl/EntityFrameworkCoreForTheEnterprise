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
                entity.ToTable("Customer", "Sales");

                entity.HasKey(p => p.CustomerID);

                entity.Property(p => p.CustomerID).UseSqlServerIdentityColumn();

                entity.Property(p => p.CompanyName).HasColumnType("varchar(100)");
                entity.Property(p => p.ContactName).HasColumnType("varchar(100)");
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                entity
                    .HasAlternateKey(p => new { p.CompanyName })
                    .HasName("U_CompanyName");
            });
        }
    }
}
