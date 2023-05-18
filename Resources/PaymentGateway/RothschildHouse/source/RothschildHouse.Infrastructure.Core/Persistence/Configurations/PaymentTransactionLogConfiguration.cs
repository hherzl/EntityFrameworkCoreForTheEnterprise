using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class PaymentTransactionLogConfiguration : AuditableEntityConfiguration<PaymentTransactionLog>
    {
        public override void Configure(EntityTypeBuilder<PaymentTransactionLog> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("PaymentTransactionLog", "dbo");

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
                .Property(p => p.PaymentTransactionId)
                .HasColumnType("bigint")
                .IsRequired()
                ;

            builder
                .Property(p => p.PaymentTransactionStatusId)
                .HasColumnType("smallint")
                .IsRequired()
                ;

            builder
                .Property(p => p.LogType)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                ;

            builder
                .Property(p => p.ContentType)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                ;

            builder
                .Property(p => p.Content)
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                ;

            builder
                .Property(p => p.Notes)
                .HasColumnType("nvarchar(max)")
                ;

            // Add configuration for foreign keys

            builder
                .HasOne(p => p.PaymentTransactionFk)
                .WithMany(b => b.PaymentTransactionLogList)
                .HasForeignKey(p => p.PaymentTransactionId)
                .HasConstraintName("FK_dbo_PaymentTransactionLog_PaymentTransactionId_dbo_PaymentTransaction")
                ;
        }
    }
}
