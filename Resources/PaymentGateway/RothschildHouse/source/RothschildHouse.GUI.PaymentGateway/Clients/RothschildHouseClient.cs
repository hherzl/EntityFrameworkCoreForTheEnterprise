using System.Net.Http.Headers;
using System.Text.Json;
using RothschildHouse.GUI.PaymentGateway.Clients.Models;
using RothschildHouse.GUI.PaymentGateway.Clients.Models.Common;

namespace RothschildHouse.GUI.PaymentGateway.Clients
{
    public class RothschildHouseClient
    {
        public const string ApplicationJson = "application/json";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public RothschildHouseClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetValue<string>("ClientEndpoints:RothschildHouse");
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("RothschildHouse");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        public async Task<ListResponse<CountryItemModel>> SearchCountriesAsync(SearchCountriesRequest query)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/search-country", query.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<CountryItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<CurrencyItemModel>> SearchCurrenciesAsync(SearchCurrenciesRequest query)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/search-currency", query.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<CurrencyItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<ClientApplicationItemModel>> SearchClientApplicationsAsync(SearchClientApplicationsRequest query)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/search-client-application", query.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<ClientApplicationItemModel>>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<ListResponse<CardItemModel>> SearchCardsAsync(SearchCardsRequest query)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsync($"{_endpoint}/search-card", query.ToStringContent(ApplicationJson));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ListResponse<CardItemModel>>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
