using System.Text.Json;
using RothschildHouse.Library.Common.Clients.Models.Reports;

namespace RothschildHouse.Library.Common.Clients
{
    public class ReportsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportsClient(IHttpClientFactory httpClientFactory)
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
            var httpClient = _httpClientFactory.CreateClient("Reports");

            return httpClient;
        }

        public async Task<MonthlySalesResponse> GetMonthlySalesAsync()
        {
            using var httpClient = CreateHttpClient();

            var response = await httpClient.GetAsync("monthly-sale");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MonthlySalesResponse>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
