using System;
using System.Collections.ObjectModel;

namespace Store.Core.EntityLayer.Sales
{
    public class OrderStatus : IAuditableEntity
    {
        public OrderStatus()
        {
        }

        public OrderStatus(Int16? orderStatusID)
        {
            OrderStatusID = orderStatusID;
        }

        public Int16? OrderStatusID { get; set; }

        public String Description { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<Order> Orders { get; set; }
    }
}
