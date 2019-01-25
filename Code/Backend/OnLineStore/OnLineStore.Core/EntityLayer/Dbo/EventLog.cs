using System;

namespace OnlineStore.Core.EntityLayer.Dbo
{
    public class EventLog : IEntity
    {
        public EventLog()
        {
        }

        public int? EventLogID { get; set; }

        public int? EventType { get; set; }

        public string Key { get; set; }

        public string Message { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}
