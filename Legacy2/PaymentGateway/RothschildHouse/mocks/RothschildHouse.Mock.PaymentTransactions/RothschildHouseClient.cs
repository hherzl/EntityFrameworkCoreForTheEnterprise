using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace RothschildHouse.Mock.PaymentTransactions;

internal record ProcessTransactionRequest
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
}

internal record ProcessTransactionResponse
{
    public long? Id { get; set; }
    public bool Successed { get; set; }
    public string Client { get; set; }
    public decimal? OrderTotal { get; set; }
    public string Currency { get; set; }
}

internal class RothschildHouseClient
{
    const string APPLICATION_JSON = "application/json";

    private readonly IConfigurationRoot _configurationRoot;
    private readonly string _endpoint;

    public RothschildHouseClient()
    {
        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            ;

        _endpoint = _configurationRoot["Clients:RothschildHouse"];
    }

    private static HttpClient CreateHttpClient()
    {
        var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPLICATION_JSON));

        return client;
    }

    static JsonSerializerOptions DefaultJsonSerializerOptions
        => new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

    public async Task<ProcessTransactionResponse> ProcessTransactionAsync(ProcessTransactionRequest request)
    {
        using var client = CreateHttpClient();

        var response = await client.PostAsJsonAsync($"{_endpoint}/process-transaction", request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProcessTransactionResponse>(content, options: DefaultJsonSerializerOptions);
    }
}
