using System.Collections.Generic;
using OnlineStore.Core.Domain.Warehouse;
using OnlineStore.Core.Domain.Sales;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Product> Products { get; set; }

        IEnumerable<Customer> Customers { get; set; }
    }
}
