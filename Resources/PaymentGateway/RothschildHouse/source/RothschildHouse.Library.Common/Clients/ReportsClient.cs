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

        public async Task<YearlySalesResponse> GetYearlySalesAsync(int year)
        {
            using var httpClient = CreateHttpClient();

            var response = await httpClient.GetAsync($"yearly-sale/{year}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<YearlySalesResponse>(content, options: DefaultJsonSerializerOptions);
        }

        public async Task<MonthlySalesResponse> GetMonthlySalesAsync(int year, int month)
        {
            using var httpClient = CreateHttpClient();

            var response = await httpClient.GetAsync($"monthly-sale/{year}/{month}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MonthlySalesResponse>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
