using System.Collections.Generic;
using OnlineStore.Core.EntityLayer.Warehouse;
using OnlineStore.Core.EntityLayer.Sales;

namespace OnlineStore.Core.BusinessLayer.Requests
{
    public class CreateOrderRequest : ICreateOrderRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
