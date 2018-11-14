using System.Collections.Generic;
using OnLineStore.Core.EntityLayer.Production;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.Core.BusinessLayer.Requests
{
    public class CreateOrderRequest : ICreateOrderRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
