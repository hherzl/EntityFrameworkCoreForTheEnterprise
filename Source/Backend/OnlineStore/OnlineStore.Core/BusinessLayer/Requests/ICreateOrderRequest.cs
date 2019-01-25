using System.Collections.Generic;
using OnlineStore.Core.EntityLayer.Warehouse;
using OnlineStore.Core.EntityLayer.Sales;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Product> Products { get; set; }

        IEnumerable<Customer> Customers { get; set; }
    }
}
