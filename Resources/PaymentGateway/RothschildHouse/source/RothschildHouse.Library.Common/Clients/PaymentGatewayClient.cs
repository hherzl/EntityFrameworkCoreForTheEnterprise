using System.Text.Json;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Library.Common.Clients
{
    public class PaymentGatewayClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentGatewayClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("PaymentGateway");

            return client;
        }

        public async Task<ListResponse<CountryItemModel>> GetCountriesAsync(GetCountriesRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"country?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ListResponse<CountryItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<CurrencyItemModel>> GetCurrenciesAsync(GetCurrenciesRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"currency?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ListResponse<CurrencyItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<ClientApplicationItemModel>> GetClientApplicationsAsync(GetClientApplicationsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"client-application?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ListResponse<ClientApplicationItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<ClientApplicationDetailsModel>> GetClientApplicationAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"client-application/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SingleResponse<ClientApplicationDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CardItemModel>> GetCardsAsync(GetCardsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"card?pageSize={request.PageSize}&pageNumber={request.PageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResponse<CardItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CardDetailsModel>> GetCardAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"card/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SingleResponse<CardDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<GetCustomersViewBagRespose> GetCustomersViewBag()
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"customer-viewbag");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetCustomersViewBagRespose>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<CustomerItemModel>> GetCustomersAsync(GetCustomersRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"customer?pageSize={request.PageSize}&pageNumber={request.PageNumber}&name={request.Name}&countryId={request.CountryID}&phone={request.Phone}&email={request.Email}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResponse<CustomerItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<CustomerDetailsModel>> GetCustomerAsync(Guid? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"customer/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SingleResponse<CustomerDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<GetTransactionsViewBagRespose> GetTransactionsViewBag()
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"transaction-viewbag");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetTransactionsViewBagRespose>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<PagedResponse<TransactionItemModel>> GetTransactionsAsync(GetTransactionsRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"transaction?pageSize={request.PageSize}&pageNumber={request.PageNumber}&transactionStatusId={request.TransactionStatusId}&clientApplicationId={request.ClientApplicationId}&startDate={request.StartDate}&endDate={request.EndDate}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedResponse<TransactionItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<SingleResponse<TransactionDetailsModel>> GetTransactionAsync(long? id)
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"transaction/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SingleResponse<TransactionDetailsModel>>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
