using System.Text.Json;
using System.Text.Json.Serialization;
using RothschildHouse.Clients.CityBank.Payloads.Avs;
using RothschildHouse.Clients.CityBank.Payloads.Cvv2;
using RothschildHouse.Clients.CityBank.Payloads.ProcessPayment;

namespace RothschildHouse.Clients.CityBank;

public record ProcessPaymentRequest
{
    public short? CardTypeId { get; set; }
    public string IssuingNetwork { get; set; }
    public string CardholderName { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string Cvv { get; set; }

    public string Address { get; set; }
    public string PostalCode { get; set; }

    public decimal? OrderTotal { get; set; }
    public string Currency { get; set; }

    public virtual string ToJson()
        => JsonSerializer.Serialize(this, GlobalJsonSerializerOptions.Default);
}

public record CardPayload
{
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; }

    [JsonPropertyName("expiry")]
    public string Expiry { get; set; }

    [JsonPropertyName("card_code")]
    public string CardCode { get; set; }
}

public record BillingAddressPayload
{
    [JsonPropertyName("street_address1")]
    public string StreetAddress1 { get; set; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }
}

public record PaymentMethodPayload
{
    [JsonPropertyName("card")]
    public CardPayload Card { get; set; }

    [JsonPropertyName("billing_address")]
    public BillingAddressPayload BillingAddress { get; set; }
}

public record ProcessPaymentResponse
{
    public static ProcessPaymentResponse Failed
        => new() { Successed = false };

    public ProcessPaymentResponse()
    {
        Authorization = new();
        Capture = new();
    }

    public bool Successed { get; set; }
    public Guid? Guid { get; set; }

    public CityBankAvsPayload AvsPayload { get; set; }
    public Cvv2Payload Cvv2Payload { get; set; }

    public ProcessPaymentAuthorization Authorization { get; set; }
    public ProcessPaymentCapture Capture { get; set; }

    public string SubscriptionTransactionId { get; set; }

    public virtual string ToJson()
        => JsonSerializer.Serialize(this, GlobalJsonSerializerOptions.Default);
}
