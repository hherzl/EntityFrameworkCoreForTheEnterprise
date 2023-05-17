using System.Net.Http.Headers;
using System.Text.Json;
using RothschildHouse.Library.Common.Clients.Contracts;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Mock.PaymentTransactions
{
    internal class RothschildHouseClient : IRothschildHouseClient
    {
        public const string ClientId = "client_id";
        public const string ClientSecret = "client_secret";

        public const string ApplicationJson = "application/json";

        private readonly string _endpoint;

        public RothschildHouseClient()
        {
            _endpoint = "https://localhost:37210/api/v1";
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add(ClientId, "Mocks");
            client.DefaultRequestHeaders.Add(ClientSecret, "B74CB3C2-BB35-4436-BCFB-8769B521CA3D");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        public Task<PagedResponse<ClientApplicationItemModel>> SearchClientApplicationsAsync(SearchClientApplicationsQuery request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<ClientApplicationDetailsModel>> GetClientApplicationAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<CardItemModel>> SearchCardsAsync(SearchCardsQuery request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<CardDetailsModel>> GetCardAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<CustomerItemModel>> SearchCustomersAsync(SearchCustomersQuery request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<CustomerDetailsModel>> GetCustomerAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<SearchPaymentTransactionsViewBagRespose> SearchPaymentTransactionsViewBag()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<PaymentTransactionItemModel>> SearchPaymentTransactionsAsync(SearchPaymentTransactionsQuery request)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<PaymentTransactionDetailsModel>> GetPaymentTransactionAsync(long? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessPaymentTransactionResponse> ProcessPaymentTransactionAsync(ProcessPaymentTransactionCommand request)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/process-payment-txn", request.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProcessPaymentTransactionResponse>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
