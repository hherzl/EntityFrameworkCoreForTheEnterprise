using System.Collections.Generic;
using Store.Core.EntityLayer.HumanResources;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Requests
{
    public class CreateOrderRequest : ICreateOrderRequest
    {
        public IEnumerable<Customer> Customers { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public IEnumerable<Shipper> Shippers { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public Order Order { get; set; }
    }
}
