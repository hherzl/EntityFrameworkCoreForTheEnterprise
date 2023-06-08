using System.Text.Json.Serialization;

namespace RothschildHouse.TP.CityBank.Payloads.Avs
{
    public record PaymentMethodPayload
    {
        [JsonPropertyName("card")]
        public CardPayload Card { get; set; }

        [JsonPropertyName("billing_address")]
        public BillingAddressPayload BillingAddress { get; set; }
    }
}
