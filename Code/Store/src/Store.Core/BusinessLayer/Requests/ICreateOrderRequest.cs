using System.Collections.Generic;
using Store.Core.EntityLayer.HumanResources;
using Store.Core.EntityLayer.Production;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Customer> Customers { get; set; }

        IEnumerable<Employee> Employees { get; set; }

        IEnumerable<Shipper> Shippers { get; set; }

        IEnumerable<Product> Products { get; set; }

        Order Order { get; set; }
    }
}
