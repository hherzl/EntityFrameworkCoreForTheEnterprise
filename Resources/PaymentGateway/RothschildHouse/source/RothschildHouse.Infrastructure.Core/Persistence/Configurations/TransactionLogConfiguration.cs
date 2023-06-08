using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class TransactionLogConfiguration : AuditableEntityConfiguration<TransactionLog>
    {
        public override void Configure(EntityTypeBuilder<TransactionLog> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("TransactionLog", "dbo");

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
                .Property(p => p.TransactionId)
                .HasColumnType("bigint")
                .IsRequired()
                ;

            builder
                .Property(p => p.TransactionStatusId)
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
                .HasOne(p => p.TransactionFk)
                .WithMany(b => b.TransactionLogList)
                .HasForeignKey(p => p.TransactionId)
                .HasConstraintName("FK_dbo_TransactionLog_TransactionId_dbo_Transaction")
                ;
        }
    }
}
