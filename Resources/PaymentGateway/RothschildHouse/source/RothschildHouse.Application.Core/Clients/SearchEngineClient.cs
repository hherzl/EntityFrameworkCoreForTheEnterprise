using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RothschildHouse.Application.Core.Clients.Models;
using RothschildHouse.Application.Core.Common;

namespace RothschildHouse.Application.Core.Clients
{
    public class SearchEngineClient
    {
        public const string ApplicationJson = "application/json";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public SearchEngineClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetSection("Clients")["SearchEngine"];
            //_endpoint = "https://localhost:7252/api/v1";
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

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        public async Task<CreatedResponse<string>> IndexSaleAsync(IndexSaleRequest request)
        {
            using var client = CreateHttpClient();

            var response = await client.PostAsJsonAsync($"{_endpoint}/sale", request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CreatedResponse<string>>(content, options: DefaultJsonSerializerOptions);
        }
    }
}
