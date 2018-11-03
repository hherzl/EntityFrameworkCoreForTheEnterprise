using System.Collections.Generic;
using OnLineStore.Core.EntityLayer.Production;
using OnLineStore.Core.EntityLayer.Sales;

namespace OnLineStore.Core.BusinessLayer.Requests
{
    public interface ICreateOrderRequest : IRequest
    {
        IEnumerable<Product> Products { get; set; }

        IEnumerable<Customer> Customers { get; set; }
    }
}
