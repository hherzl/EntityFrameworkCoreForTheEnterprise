using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Dbo;

namespace OnlineStore.Core.Domain.Configurations
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            // Mapping for table
            builder.ToTable("EventLog", "dbo");

            // Set key for entity
            builder.HasKey(p => p.ID);

            // Set mapping for columns
            builder.Property(p => p.EventType).HasColumnType("int").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.Message).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.EntryDate).HasColumnType("datetime").IsRequired();
        }
    }
}
