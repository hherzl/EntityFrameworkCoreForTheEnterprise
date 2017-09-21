using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Mapping.Dbo
{
    [Export(typeof(IEntityMap))]
    public class CountryMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                // Mapping for table
                entity.ToTable("Country", "dbo");

                // Set key for entity
                entity.HasKey(p => p.CountryID);

                // Set identity for entity (auto increment)
                entity.Property(p => p.CountryID).UseSqlServerIdentityColumn();

                // Set mapping for columns
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                // Set concurrency token for entity
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            });
        }
    }
}
