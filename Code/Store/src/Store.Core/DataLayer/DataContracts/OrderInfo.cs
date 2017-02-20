using System;

namespace Store.Core.DataLayer.DataContracts
{
    public class OrderInfo
    {
        public OrderInfo()
        {
        }

        public Int32? OrderID { get; set; }

        public Int16? OrderStatusID { get; set; }

        public String OrderStatusDescription { get; set; }

        public DateTime? OrderDate { get; set; }

        public Int32? CustomerID { get; set; }

        public String CustomerName { get; set; }

        public Int32? EmployeeID { get; set; }

        public String EmployeeName { get; set; }

        public Int32? ShipperID { get; set; }

        public String ShipperName { get; set; }
         
        public Decimal? Total { get; set; }

        public String Comments { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
