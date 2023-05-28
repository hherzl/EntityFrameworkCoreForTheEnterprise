using System.Net.Http.Headers;
using System.Text.Json;

namespace RothschildHouse.GUI.PaymentGateway.Clients
{
    public class RothschildHouseReportsClient
    {
        public const string ApplicationJson = "application/json";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _endpoint;

        public RothschildHouseReportsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = configuration.GetValue<string>("ClientEndpoints:RothschildHouseReports");
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

    public record MonthlySalesResponse
    {
        public List<string> Months { get; set; }
        public List<MonthlySaleItemModel> Sales { get; set; }
    }

    public record MonthlySaleItemModel
    {
        public MonthlySaleItemModel()
        {
            Values = new();
        }

        public string Year { get; set; }
        public string Month { get; set; }
        public string ClientApplication { get; set; }
        public List<double> Values { get; set; }
    }
}
