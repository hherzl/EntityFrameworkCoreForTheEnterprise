using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.EntityLayer.Warehouse;

namespace OnlineStore.Core.DataLayer.Configurations.Warehouse
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            // Mapping for table
            builder.ToTable("Location", "Warehouse");

            // Set key for entity
            builder.HasKey(p => p.LocationID);

            // Set mapping for columns
            builder.Property(p => p.LocationID).HasColumnType("varchar(5)").IsRequired();
            builder.Property(p => p.LocationName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
