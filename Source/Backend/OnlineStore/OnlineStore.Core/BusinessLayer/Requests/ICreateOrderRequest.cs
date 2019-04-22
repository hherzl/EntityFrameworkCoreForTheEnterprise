using System.Collections.Generic;
using OnlineStore.Core.DomainDrivenDesign.Warehouse;
using OnlineStore.Core.DomainDrivenDesign.Sales;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Product> Products { get; set; }

        IEnumerable<Customer> Customers { get; set; }
    }
}
