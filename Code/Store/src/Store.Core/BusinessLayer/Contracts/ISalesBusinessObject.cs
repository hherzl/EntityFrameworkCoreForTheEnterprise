using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface ISalesBusinessObject : IBusinessObject
    {
        IListModelResponse<Customer> GetCustomers(Int32 pageSize, Int32 pageNumber);

        IListModelResponse<Shipper> GetShippers(Int32 pageSize, Int32 pageNumber);

        Task<IListModelResponse<Order>> GetOrders(Int32 pageSize, Int32 pageNumber);

        ISingleModelResponse<Order> GetOrder(Int32 id);

        ISingleModelResponse<Order> CreateOrder(Order header, OrderDetail[] details);

        ISingleModelResponse<Order> CloneOrder(Int32 id);
    }
}
