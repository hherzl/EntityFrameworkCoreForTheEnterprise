using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Mapping
{
    [Export(typeof(IEntityMap))]
    public class EventLogMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventLog>();

            entity.ToTable("EventLog", "dbo");

            entity.HasKey(p => p.EventLogID);

            entity.Property(p => p.EventType).HasColumnType("int").IsRequired();

            entity.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();

            entity.Property(p => p.Message).HasColumnType("varchar(max)").IsRequired();

            entity.Property(p => p.EntryDate).HasColumnType("datetime").IsRequired();
        }
    }
}
