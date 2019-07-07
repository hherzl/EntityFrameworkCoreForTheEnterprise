using System;
using System.Collections.ObjectModel;

namespace OnlineStore.Core.Domain.Sales
{
    public class OrderStatus : IAuditableEntity
    {
        public OrderStatus()
        {
        }

        public OrderStatus(short? id)
        {
            ID = id;
        }

        public short? ID { get; set; }

        public string Description { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual Collection<OrderHeader> Orders { get; set; }
    }
}
