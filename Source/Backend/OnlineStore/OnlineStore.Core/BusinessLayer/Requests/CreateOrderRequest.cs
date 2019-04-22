using System.Collections.Generic;
using OnlineStore.Core.DomainDrivenDesign.Warehouse;
using OnlineStore.Core.DomainDrivenDesign.Sales;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public class CreateOrderRequest : ICreateOrderRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
