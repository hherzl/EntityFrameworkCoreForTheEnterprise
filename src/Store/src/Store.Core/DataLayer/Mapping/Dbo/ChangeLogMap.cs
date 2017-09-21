using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Mapping
{
    [Export(typeof(IEntityMap))]
    public class ChangeLogMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangeLog>(entity =>
            {
                // Mapping for table
                entity.ToTable("ChangeLog", "dbo");

                // Set key for entity
                entity.HasKey(p => p.ChangeLogID);

                // Set identity for entity (auto increment)
                entity.Property(p => p.ChangeLogID).UseSqlServerIdentityColumn();

                // Set mapping for columns
                entity.Property(p => p.ClassName).HasColumnType("varchar(128)").IsRequired();
                entity.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
                entity.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
                entity.Property(p => p.OriginalValue).HasColumnType("varchar(max)");
                entity.Property(p => p.CurrentValue).HasColumnType("varchar(max)");
                entity.Property(p => p.UserName).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.ChangeDate).HasColumnType("varchar(128)").IsRequired();
            });
        }
    }
}
