using System;

namespace Store.Core.EntityLayer
{
    public class EventLog : IEntity
    {
        public EventLog()
        {
        }

        public Int32? EventLogID { get; set; }

        public Int32? EventType { get; set; }

        public String Key { get; set; }

        public String Message { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}
