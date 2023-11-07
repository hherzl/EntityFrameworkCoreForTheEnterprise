using System.Net.Http.Json;
using System.Text.Json;
using RothschildHouse.Library.Common.Clients.Models.Common;
using RothschildHouse.Library.Common.Clients.Models.SearchEngine;

namespace RothschildHouse.Library.Common.Clients
{
    public class SearchEngineClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SearchEngineClient(IHttpClientFactory httpClientFactory)
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
            var client = _httpClientFactory.CreateClient("SearchEngine");

            return client;
        }

        public async Task<CreatedResponse<string>> IndexSaleAsync(IndexSaleRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsJsonAsync($"sale", request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CreatedResponse<string>>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
