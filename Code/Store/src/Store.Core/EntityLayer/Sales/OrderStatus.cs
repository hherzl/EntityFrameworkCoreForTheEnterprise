using System;

namespace Store.Core.EntityLayer.Sales
{
    public class OrderStatus
    {
        public OrderStatus()
        {
        }

        public Int16? OrderStatusID { get; set; }

        public String Description { get; set; }
    }
}
