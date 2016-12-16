using System;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer
{
    public interface ISalesBusinessObject : IBusinessObject
    {
        IListModelResponse<Customer> GetCustomers(Int32 pageSize, Int32 pageNumber);

        IListModelResponse<Shipper> GetShippers(Int32 pageSize, Int32 pageNumber);

        IListModelResponse<Order> GetOrders(Int32 pageSize, Int32 pageNumber);

        ISingleModelResponse<Order> CreateOrder(Order header, OrderDetail[] details);
    }
}
