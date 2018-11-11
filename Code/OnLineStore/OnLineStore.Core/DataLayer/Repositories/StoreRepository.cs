using System.Linq;
using OnLineStore.Core.DataLayer.Contracts;
using OnLineStore.Core.EntityLayer.Dbo;

namespace OnLineStore.Core.DataLayer.Repositories
{
    public class StoreRepository : Repository, IStoreRepository
    {
        public StoreRepository(UserInfo userInfo, OnLineStoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<EventLog> GetEventLogs()
            => DbContext.EventLogs;

        public EventLog GetEventLog(EventLog entity)
            => DbContext.EventLogs.FirstOrDefault(item => item.EventLogID == entity.EventLogID);
    }
}
