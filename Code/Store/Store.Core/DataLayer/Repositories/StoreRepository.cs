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

        public IQueryable<EventLog> GetEventLogs()
            => DbContext.Set<EventLog>();

        public EventLog GetEventLog(EventLog entity)
            => DbContext.Set<EventLog>().FirstOrDefault(item => item.EventLogID == entity.EventLogID);
    }
}
