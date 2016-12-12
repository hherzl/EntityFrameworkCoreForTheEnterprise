using System.Collections.Generic;
using System.Linq;
using Store.Core.DataLayer.Contracts;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Repositories
{
    public class StoreRepository : Repository, IStoreRepository
    {
        public StoreRepository(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<EventLog> GetEventLogs()
        {
            return DbContext.Set<EventLog>();
        }

        public EventLog GetEventLog(EventLog entity)
        {
            return DbContext.Set<EventLog>().FirstOrDefault(item => item.EventLogID == entity.EventLogID);
        }

        public void AddEventLog(EventLog entity)
        {
            DbContext.Set<EventLog>().Add(entity);

            DbContext.SaveChanges();
        }

        public void UpdateEventLog(EventLog changes)
        {
            var entity = GetEventLog(changes);

            if (entity != null)
            {
                entity.EventType = changes.EventType;
                entity.Key = changes.Key;
                entity.Message = changes.Message;
                entity.EntryDate = changes.EntryDate;

                DbContext.SaveChanges();
            }
        }

        public void DeleteEventLog(EventLog entity)
        {
            DbContext.Set<EventLog>().Remove(entity);

            DbContext.SaveChanges();
        }
    }
}
