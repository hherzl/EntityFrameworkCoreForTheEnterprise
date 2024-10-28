using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Entities;
using RothschildHouse.Infrastructure.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Persistence.Configurations;

internal class CustomerConfiguration : AuditableEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("Customer", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            ;

        builder
            .Property(p => p.PersonId)
            .HasColumnType("int")
            ;

        builder
            .Property(p => p.CompanyId)
            .HasColumnType("int")
            ;

        builder
            .Property(p => p.CountryId)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.AddressLine1)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.AddressLine2)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.Phone)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            ;

        builder
            .Property(p => p.Email)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.AlienGuid)
            .HasColumnType("uniqueidentifier")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.PersonFk)
            .WithMany(b => b.CustomerList)
            .HasForeignKey(p => p.PersonId)
            .HasConstraintName("FK_dbo_Customer_PersonId_dbo_Person")
            ;

        builder
            .HasOne(p => p.CompanyFk)
            .WithMany(b => b.CustomerList)
            .HasForeignKey(p => p.CompanyId)
            .HasConstraintName("FK_dbo_Customer_CompanyId_dbo_Company")
            ;

        builder
            .HasOne(p => p.CountryFk)
            .WithMany(b => b.CustomerList)
            .HasForeignKey(p => p.CountryId)
            .HasConstraintName("FK_dbo_Customer_CountryId_dbo_Country")
            ;
    }
}
