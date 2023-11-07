using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class CardConfiguration : AuditableEntityConfiguration<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("Card", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.CardTypeId)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.IssuingNetwork)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                .IsRequired()
                ;

            builder
                .Property(p => p.CardholderName)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                ;

            builder
                .Property(p => p.CardNumber)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired()
                ;

            builder
                .Property(p => p.ExpirationDate)
                .HasColumnType("nvarchar")
                .HasMaxLength(6)
                .IsRequired()
                ;

            builder
                .Property(p => p.Cvv)
                .HasColumnType("nvarchar")
                .HasMaxLength(4)
                .IsRequired()
                ;
        }
    }
}
