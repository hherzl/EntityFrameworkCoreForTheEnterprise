using System.Collections.Generic;
using System.Linq;
using Store.Core.DataLayer.Contracts;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Repositories
{
    public class StoreRepository : Repository, IStoreRepository
    {
        public StoreRepository(UserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IEnumerable<EventLog> GetEventLogs()
            => DbContext.Set<EventLog>();

        public EventLog GetEventLog(EventLog entity)
            => DbContext.Set<EventLog>().FirstOrDefault(item => item.EventLogID == entity.EventLogID);

        public void AddEventLog(EventLog entity)
        {
            Add(entity);

            CommitChanges();
        }

        public void UpdateEventLog(EventLog changes)
        {
            Update(changes);

            CommitChanges();
        }

        public void DeleteEventLog(EventLog entity)
        {
            Remove(entity);

            CommitChanges();
        }
    }
}
