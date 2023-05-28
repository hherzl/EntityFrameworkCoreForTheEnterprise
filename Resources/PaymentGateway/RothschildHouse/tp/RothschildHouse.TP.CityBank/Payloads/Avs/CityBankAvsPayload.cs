using System.Text.Json;
using System.Text.Json.Serialization;

namespace RothschildHouse.TP.CityBank.Payloads.Avs
{
    internal record CityBankAvsPayload
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        [JsonPropertyName("paymentMethod")]
        public PaymentMethodPayload PaymentMethodPayload { get; set; }

        public string ToJson()
            => JsonSerializer.Serialize(this, GlobalJsonSerializerOptions.Default);
    }
}
