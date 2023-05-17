using System.Collections.Generic;
using OnlineStore.Core.Domain.Warehouse;
using OnlineStore.Core.Domain.Sales;

namespace OnlineStore.Core.Business.Requests
{
    public class CreateOrderRequest : ICreateOrderRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
