using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.Contracts
{
    public interface IRothschildHouseClient
    {
        Task<PagedResponse<ClientApplicationItemModel>> SearchClientApplicationsAsync(SearchClientApplicationsQuery request);

        Task<SingleResponse<ClientApplicationDetailsModel>> GetClientApplicationAsync(Guid? id);

        Task<PagedResponse<CardItemModel>> SearchCardsAsync(SearchCardsQuery request);

        Task<SingleResponse<CardDetailsModel>> GetCardAsync(Guid? id);

        Task<PagedResponse<CustomerItemModel>> SearchCustomersAsync(SearchCustomersQuery request);

        Task<SingleResponse<CustomerDetailsModel>> GetCustomerAsync(Guid? id);

        Task<SearchPaymentTransactionsViewBagRespose> SearchPaymentTransactionsViewBag();

        Task<PagedResponse<PaymentTransactionItemModel>> SearchPaymentTransactionsAsync(SearchPaymentTransactionsQuery request);

        Task<SingleResponse<PaymentTransactionDetailsModel>> GetPaymentTransactionAsync(long? id);

        Task<ProcessPaymentTransactionResponse> ProcessPaymentTransactionAsync(ProcessPaymentTransactionCommand request);
    }
}
