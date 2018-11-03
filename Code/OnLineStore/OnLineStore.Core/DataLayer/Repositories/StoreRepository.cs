using System.Linq;
using OnLineStore.Core.DataLayer.Contracts;
using OnLineStore.Core.EntityLayer.Dbo;

namespace OnLineStore.Core.DataLayer.Repositories
{
    public class StoreRepository : Repository, IStoreRepository
    {
        public StoreRepository(UserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<EventLog> GetEventLogs()
            => DbContext.Set<EventLog>();

        public EventLog GetEventLog(EventLog entity)
            => DbContext.Set<EventLog>().FirstOrDefault(item => item.EventLogID == entity.EventLogID);
    }
}
