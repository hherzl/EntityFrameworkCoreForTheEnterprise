using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface ISalesBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Customer>> GetCustomersAsync(Int32 pageSize, Int32 pageNumber);

        Task<IListModelResponse<Shipper>> GetShippersAsync(Int32 pageSize, Int32 pageNumber);

        Task<IListModelResponse<Order>> GetOrdersAsync(Int32 pageSize, Int32 pageNumber);

        Task<ISingleModelResponse<Order>> GetOrderAsync(Int32 id);

        Task<ISingleModelResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details);

        Task<ISingleModelResponse<Order>> CloneOrderAsync(Int32 id);
    }
}
