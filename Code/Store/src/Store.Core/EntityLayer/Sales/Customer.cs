using System;

namespace Store.Core.EntityLayer.Sales
{
    public class Customer : IAuditEntity
    {
        public Customer()
        {
        }

        public Customer(Int32? customerID)
        {
            CustomerID = customerID;
        }

        public Int32? CustomerID { get; set; }

        public String CompanyName { get; set; }

        public String ContactName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
