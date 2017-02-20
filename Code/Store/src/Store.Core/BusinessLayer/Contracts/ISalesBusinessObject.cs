using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface ISalesBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Customer>> GetCustomersAsync(Int32 pageSize, Int32 pageNumber);

        Task<IListModelResponse<Shipper>> GetShippersAsync(Int32 pageSize, Int32 pageNumber);

        Task<IListModelResponse<OrderInfo>> GetOrdersAsync(Int32 pageSize, Int32 pageNumber, Int32? customerID = null, Int32? employeeID = null, Int32? shipperID = null);

        Task<ISingleModelResponse<Order>> GetOrderAsync(Int32 id);

        Task<ISingleModelResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details);

        Task<ISingleModelResponse<Order>> CloneOrderAsync(Int32 id);

        Task<ISingleModelResponse<Order>> RemoveOrderAsync(Int32 id);
    }
}
