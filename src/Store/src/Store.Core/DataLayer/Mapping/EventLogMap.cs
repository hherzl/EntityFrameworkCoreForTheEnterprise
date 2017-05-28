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

            entity.Property(p => p.EventLogID).UseSqlServerIdentityColumn();
        }
    }
}
