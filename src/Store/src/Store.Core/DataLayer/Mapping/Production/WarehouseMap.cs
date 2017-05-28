using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    [Export(typeof(IEntityMap))]
    public class WarehouseMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Warehouse>();

            entity.ToTable("Warehouse", "Production");

            entity.HasKey(p => p.WarehouseID);

            entity.Property(p => p.WarehouseName).HasColumnType("varchar(100)").IsRequired();

            entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");

            entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            entity
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
