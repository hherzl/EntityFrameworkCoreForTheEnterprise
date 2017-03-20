using System.Collections.Generic;
using Store.Core.EntityLayer;

namespace Store.Core.DataLayer.Contracts
{
    public interface IStoreRepository : IRepository
    {
        IEnumerable<EventLog> GetEventLogs();

        EventLog GetEventLog(EventLog entity);

        void AddEventLog(EventLog entity);

        void UpdateEventLog(EventLog changes);

        void DeleteEventLog(EventLog entity);
    }
}
