using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class VTransactionTypeConfiguration : IEntityTypeConfiguration<VTransactionType>
    {
        public void Configure(EntityTypeBuilder<VTransactionType> builder)
        {
            // Set configuration for entity
            builder.ToView("VTransactionType", "dbo");

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
}
