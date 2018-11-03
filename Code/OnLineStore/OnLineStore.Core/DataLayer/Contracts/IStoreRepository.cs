using System.Linq;
using OnLineStore.Core.EntityLayer.Dbo;

namespace OnLineStore.Core.DataLayer.Contracts
{
    public interface IStoreRepository : IRepository
    {
        IQueryable<EventLog> GetEventLogs();

        EventLog GetEventLog(EventLog entity);
    }
}
