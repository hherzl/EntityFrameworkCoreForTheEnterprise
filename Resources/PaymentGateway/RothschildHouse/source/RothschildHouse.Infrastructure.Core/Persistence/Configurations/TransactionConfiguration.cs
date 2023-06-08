using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class TransactionConfiguration : AuditableEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("Transaction", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set identity for entity (auto increment)
            builder.Property(p => p.Id).UseIdentityColumn();

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("bigint")
                .IsRequired()
                ;

            builder
                .Property(p => p.Guid)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.TransactionDateTime)
                .HasColumnType("datetime")
                ;

            builder
                .Property(p => p.TransactionTypeId)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.TransactionStatusId)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.ClientApplicationId)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.ClientFullClassName)
                .HasColumnType("nvarchar")
                .HasMaxLength(511)
                .IsRequired()
                ;

            builder
                .Property(p => p.CustomerId)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.StoreId)
                .HasColumnType("int")
                .IsRequired()
                ;

            builder
                .Property(p => p.CardId)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.Amount)
                .HasColumnType("decimal(12, 4)")
                .IsRequired()
                ;

            builder
                .Property(p => p.CurrencyId)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.CurrencyRate)
                .HasColumnType("decimal(18, 4)")
                .IsRequired()
                ;

            builder
                .Property(p => p.Notes)
                .HasColumnType("nvarchar(max)")
                ;

            // Add configuration for uniques

            builder
                .HasIndex(p => p.Guid)
                .IsUnique()
                .HasDatabaseName("UQ_dbo_Transaction_Guid")
                ;

            // Add configuration for foreign keys

            builder
                .HasOne(p => p.ClientApplicationFk)
                .WithMany(b => b.TransactionList)
                .HasForeignKey(p => p.ClientApplicationId)
                .HasConstraintName("FK_dbo_Transaction_ClientApplicationId_dbo_ClientApplication")
                ;

            builder
                .HasOne(p => p.CustomerFk)
                .WithMany(b => b.TransactionList)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_dbo_Transaction_CustomerId_dbo_Customer")
                ;

            builder
                .HasOne(p => p.CardFk)
                .WithMany(b => b.TransactionList)
                .HasForeignKey(p => p.CardId)
                .HasConstraintName("FK_dbo_Transaction_CardId_dbo_Card")
                ;

            builder
                .HasOne(p => p.CurrencyFk)
                .WithMany(b => b.TransactionList)
                .HasForeignKey(p => p.CurrencyId)
                .HasConstraintName("FK_dbo_Transaction_CurrencyId_dbo_Currency")
                ;
        }
    }
}
