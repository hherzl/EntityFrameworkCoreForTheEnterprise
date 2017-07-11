using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class ShipperMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Shipper>();

            entity.ToTable("Shipper", "Sales");

            entity.HasKey(p => p.ShipperID);

            entity.Property(p => p.ShipperID).UseSqlServerIdentityColumn();

            entity.HasAlternateKey(p => new { p.CompanyName }).HasName("U_CompanyName");

            entity.Property(p => p.CompanyName).HasColumnType("varchar(100)");

            entity.Property(p => p.ContactName).HasColumnType("varchar(100)");

            entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");

            entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
