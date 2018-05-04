using System;
using System.Threading.Tasks;
using Store.Core.BusinessLayer.Requests;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Dbo;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.BusinessLayer.Contracts
{
    public interface ISalesService : IService
    {
        Task<IPagedResponse<Customer>> GetCustomersAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IPagedResponse<Shipper>> GetShippersAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IPagedResponse<Currency>> GetCurrenciesAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IPagedResponse<PaymentMethod>> GetPaymentMethodsAsync(Int32 pageSize = 10, Int32 pageNumber = 1);

        Task<IPagedResponse<OrderInfo>> GetOrdersAsync(Int32 pageSize = 10, Int32 pageNumber = 1, Int16? currencyID = null, Int32? customerID = null, Int32? employeeID = null, Int16? orderStatusID = null, Guid? paymentMethodID = null, Int32? shipperID = null);

        Task<ISingleResponse<Order>> GetOrderAsync(Int64 id);

        Task<ISingleResponse<CreateOrderRequest>> GetCreateOrderRequestAsync();

        Task<ISingleResponse<Order>> CreateOrderAsync(Order header, OrderDetail[] details);

        Task<ISingleResponse<Order>> CloneOrderAsync(Int32 id);

        Task<ISingleResponse<Order>> RemoveOrderAsync(Int32 id);
    }
}
