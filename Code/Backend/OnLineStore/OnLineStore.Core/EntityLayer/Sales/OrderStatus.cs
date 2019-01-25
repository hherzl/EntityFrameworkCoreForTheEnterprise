using System;
using System.Collections.ObjectModel;

namespace OnlineStore.Core.EntityLayer.Sales
{
    public class OrderStatus : IAuditableEntity
    {
        public OrderStatus()
        {
        }

        public OrderStatus(short? orderStatusID)
        {
            OrderStatusID = orderStatusID;
        }

        public short? OrderStatusID { get; set; }

        public string Description { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual Collection<OrderHeader> Orders { get; set; }
    }
}
