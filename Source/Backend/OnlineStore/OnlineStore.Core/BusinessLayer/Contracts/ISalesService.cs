using System;
using System.Threading.Tasks;
using OnlineStore.Core.BusinessLayer.Requests;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.DataLayer.Sales;
using OnlineStore.Core.EntityLayer.Dbo;
using OnlineStore.Core.EntityLayer.Sales;

namespace OnlineStore.Core.BusinessLayer.Contracts
{
    public interface ISalesService : IService
    {
        Task<IPagedResponse<Customer>> GetCustomersAsync(int pageSize = 10, int pageNumber = 1);

        Task<IPagedResponse<Shipper>> GetShippersAsync(int pageSize = 10, int pageNumber = 1);

        Task<IPagedResponse<Currency>> GetCurrenciesAsync(int pageSize = 10, int pageNumber = 1);

        Task<IPagedResponse<PaymentMethod>> GetPaymentMethodsAsync(int pageSize = 10, int pageNumber = 1);

        Task<IPagedResponse<OrderInfo>> GetOrdersAsync(int pageSize = 10, int pageNumber = 1, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, string currencyID = null, Guid? paymentMethodID = null);

        Task<ISingleResponse<OrderHeader>> GetOrderAsync(long id);

        Task<ISingleResponse<CreateOrderRequest>> GetCreateOrderRequestAsync();

        Task<ISingleResponse<OrderHeader>> CreateOrderAsync(OrderHeader header, OrderDetail[] details);

        Task<ISingleResponse<OrderHeader>> CloneOrderAsync(long id);

        Task<IResponse> RemoveOrderAsync(long id);
    }
}
