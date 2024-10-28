using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Entities;

namespace RothschildHouse.Infrastructure.Persistence.Configurations;

internal class VTransactionStatusConfiguration : IEntityTypeConfiguration<VTransactionStatus>
{
    public void Configure(EntityTypeBuilder<VTransactionStatus> builder)
    {
        // Set configuration for entity
        builder.ToView("VTransactionStatus", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired()
            ;
    }
}
