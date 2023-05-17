using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.API.PaymentGateway.Domain.Entities;

namespace RothschildHouse.API.PaymentGateway.Infrastructure.Persistence.Configurations
{
    internal class PersonConfiguration : AuditableEntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("Person", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set identity for entity (auto increment)
            builder.Property(p => p.Id).UseIdentityColumn();

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("int")
                .IsRequired()
                ;

            builder
                .Property(p => p.GivenName)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                .IsRequired()
                ;

            builder
                .Property(p => p.MiddleName)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                ;

            builder
                .Property(p => p.FamilyName)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                .IsRequired()
                ;

            builder
                .Property(p => p.FullName)
                .HasColumnType("nvarchar")
                .HasMaxLength(75)
                .IsRequired()
                ;

            builder
                .Property(p => p.BirthDate)
                .HasColumnType("datetime")
                ;

            builder
                .Property(p => p.Gender)
                .HasColumnType("nvarchar")
                .HasMaxLength(1)
                ;
        }
    }
}
