using System.Net.Http.Headers;
using System.Text.Json;
using RothschildHouse.GUI.PaymentGateway.Clients.Models.Reports;

namespace RothschildHouse.GUI.PaymentGateway.Clients
{
    public class ReportsClient
    {
        public const string ApplicationJson = "application/json";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public ReportsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetValue<string>("ClientEndpoints:Reports");
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        private HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("RothschildHouseReports");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        public async Task<MonthlySalesResponse> GetMonthlySalesAsync()
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync($"{_endpoint}/monthly-sale");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MonthlySalesResponse>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
