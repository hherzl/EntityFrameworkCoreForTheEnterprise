using System;

namespace Store.Core.EntityLayer.Sales
{
    public class Customer : IEntity
    {
        public Customer()
        {
        }

        public Int32? CustomerID { get; set; }

        public String CompanyName { get; set; }

        public String ContactName { get; set; }
    }
}
