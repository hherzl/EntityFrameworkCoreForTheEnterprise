using System.Linq;
using Store.Core.EntityLayer.Dbo;

namespace Store.Core.DataLayer.Contracts
{
    public interface IStoreRepository : IRepository
    {
        IQueryable<EventLog> GetEventLogs();

        EventLog GetEventLog(EventLog entity);
    }
}
