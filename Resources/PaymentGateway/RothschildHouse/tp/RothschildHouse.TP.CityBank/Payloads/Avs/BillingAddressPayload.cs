using System.Text.Json.Serialization;

namespace RothschildHouse.TP.CityBank.Payloads.Avs
{
    public record BillingAddressPayload
    {
        [JsonPropertyName("street_address1")]
        public string StreetAddress1 { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }
    }
}
