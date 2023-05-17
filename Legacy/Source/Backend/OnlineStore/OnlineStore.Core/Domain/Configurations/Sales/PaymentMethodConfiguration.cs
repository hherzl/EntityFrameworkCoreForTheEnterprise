using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Sales;

namespace OnlineStore.Core.Domain.Configurations.Sales
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Mapping for table
            builder.ToTable("PaymentMethod", "Sales");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set mapping for columns
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
