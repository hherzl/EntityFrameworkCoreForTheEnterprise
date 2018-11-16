using System.Collections.Generic;
using OnLineStore.Core.EntityLayer.Warehouse;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Product> Products { get; set; }

        IEnumerable<Customer> Customers { get; set; }
    }
}
