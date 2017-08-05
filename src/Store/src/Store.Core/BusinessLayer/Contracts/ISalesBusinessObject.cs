using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Dbo;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface ISalesBusinessObject : IBusinessObject
    {
        Task<IListModelResponse<Customer>> GetCustomersAsync(Int32 pageSize = 0, Int32 pageNumber = 0);

        Task<IListModelResponse<Shipper>> GetShippersAsync(Int32 pageSize = 0, Int32 pageNumber = 0);

        Task<IPagingModelResponse<OrderInfo>> GetOrdersAsync(Int32 pageSize = 10, Int32 pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null);

        Task<ISingleModelResponse<Order>> GetOrderAsync(Int32 id);

        Task<ISingleModelResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details);

        Task<ISingleModelResponse<Order>> CloneOrderAsync(Int32 id);

        Task<ISingleModelResponse<Order>> RemoveOrderAsync(Int32 id);

        Task<IListModelResponse<Currency>> GetCurrenciesAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IListModelResponse<PaymentMethod>> GetPaymentMethodsAsync(Int32 pageSize = 10, Int32 pageNumber = 1);
    }
}
