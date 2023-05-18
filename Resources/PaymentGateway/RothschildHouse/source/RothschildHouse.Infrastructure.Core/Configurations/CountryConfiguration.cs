using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Configurations
{
    internal class CountryConfiguration : EntityConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("Country", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set identity for entity (auto increment)
            builder.Property(p => p.Id).UseIdentityColumn();

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired()
                ;

            builder
                .Property(p => p.TwoLetterIsoCode)
                .HasColumnType("nvarchar")
                .HasMaxLength(2)
                .IsRequired()
                ;

            builder
                .Property(p => p.ThreeLetterIsoCode)
                .HasColumnType("nvarchar")
                .HasMaxLength(3)
                .IsRequired()
                ;

            // Add configuration for uniques

            builder
                .HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("UQ_dbo_Country_Name")
                ;

            builder
                .HasIndex(p => p.TwoLetterIsoCode)
                .IsUnique()
                .HasDatabaseName("UQ_dbo_Country_TwoLetterIsoCode")
                ;

            builder
                .HasIndex(p => p.ThreeLetterIsoCode)
                .IsUnique()
                .HasDatabaseName("UQ_dbo_Country_ThreeLetterIsoCode")
                ;
        }
    }
}
