using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.HumanResources;

namespace OnlineStore.Core.Domain.Configurations.HumanResources
{
    public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
    {
        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            // Set configuration for entity
            builder.ToTable("EmployeeAddress", "HumanResources");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ID).UseSqlServerIdentityColumn();

            // Set configuration for columns
            builder.Property(p => p.ID).HasColumnType("int").IsRequired();
            builder.Property(p => p.EmployeeID).HasColumnType("int").IsRequired();
            builder.Property(p => p.AddressLine1).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.AddressLine2).HasColumnType("varchar(50)");
            builder.Property(p => p.City).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.State).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.ZipCode).HasColumnType("varchar(5)");
            builder.Property(p => p.CountryID).HasColumnType("int").IsRequired();
            builder.Property(p => p.PhoneNumber).HasColumnType("varchar(25)");
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");
            builder.Property(p => p.Timestamp).HasColumnType("timestamp");

            // Set concurrency token for entity
            builder
                .Property(p => p.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
