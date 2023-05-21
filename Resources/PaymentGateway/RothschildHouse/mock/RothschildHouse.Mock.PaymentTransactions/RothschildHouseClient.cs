using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RothschildHouse.Mock.PaymentTransactions
{
    internal record ProcessPaymentTransactionCommand
    {
        public Guid? ClientApplication { get; set; }
        public Guid? CustomerGuid { get; set; }
        public int? StoreId { get; set; }

        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public Guid? OrderGuid { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string Notes { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);
    }

    internal record ProcessPaymentTransactionResponse
    {
        public long? Id { get; set; }
        public bool Successed { get; set; }
        public string Client { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
    }

    internal class RothschildHouseClient
    {
        public const string ApplicationJson = "application/json";

        private readonly string _endpoint;

        public RothschildHouseClient()
        {
            _endpoint = "https://localhost:37210/api/v1";
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

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
