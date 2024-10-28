using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.API.PaymentGateway.Domain.Entities;

namespace RothschildHouse.API.PaymentGateway.Infrastructure.Persistence.Configurations
{
    internal class CurrencyConfiguration : AuditableEntityConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("Currency", "dbo");

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
                .HasMaxLength(50)
                .IsRequired()
                ;

            builder
                .Property(p => p.Code)
                .HasColumnType("nvarchar")
                .HasMaxLength(5)
                .IsRequired()
                ;

            builder
                .Property(p => p.Rate)
                .HasColumnType("decimal(18, 4)")
                .IsRequired()
                ;
        }
    }
}
