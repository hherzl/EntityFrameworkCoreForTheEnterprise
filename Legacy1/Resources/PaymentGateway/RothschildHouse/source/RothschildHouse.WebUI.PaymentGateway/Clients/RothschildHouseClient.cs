using System.Net.Http.Headers;
using System.Text.Json;
using RothschildHouse.Library.Common.Clients.Contracts;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.WebUI.PaymentGateway.Clients
{
    public class RothschildHouseClient : IRothschildHouseClient
    {
        public const string ApplicationJson = "application/json";

        public const string ClientId = "client_id";
        public const string ClientSecret = "client_secret";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public RothschildHouseClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetValue<string>("ClientEndpoints:RothschildHouse");
        }

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("RothschildHouse");

            client.DefaultRequestHeaders.Add(ClientId, "RothschildHouseGUI");
            client.DefaultRequestHeaders.Add(ClientSecret, "D4159097-96BE-43E0-9E8F-ED4384B0F9C2");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        public async Task<PagedResponse<ClientApplicationItemModel>> SearchClientApplicationsAsync(SearchClientApplicationsQuery request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/search-client-application?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<ClientApplicationItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<ClientApplicationDetailsModel>> GetClientApplicationAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/client-application/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<ClientApplicationDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CardItemModel>> SearchCardsAsync(SearchCardsQuery request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/search-card?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<CardItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CardDetailsModel>> GetCardAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/card/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<CardDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CustomerItemModel>> SearchCustomersAsync(SearchCustomersQuery request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/search-customer?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<CustomerItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CustomerDetailsModel>> GetCustomerAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/customer/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<CustomerDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SearchPaymentTransactionsViewBagRespose> SearchPaymentTransactionsViewBag()
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/search-payment-txn-viewbag");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SearchPaymentTransactionsViewBagRespose>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<PaymentTransactionItemModel>> SearchPaymentTransactionsAsync(SearchPaymentTransactionsQuery request)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/search-payment-txn", request.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PagedResponse<PaymentTransactionItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<PaymentTransactionDetailsModel>> GetPaymentTransactionAsync(long? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/payment-txn/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SingleResponse<PaymentTransactionDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public Task<ProcessPaymentTransactionResponse> ProcessPaymentTransactionAsync(ProcessPaymentTransactionCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
