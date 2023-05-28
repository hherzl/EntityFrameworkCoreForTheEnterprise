using System.Text.Json.Serialization;

namespace RothschildHouse.TP.CityBank.Payloads.Avs
{
    internal record BillingAddressPayload
    {
        [JsonPropertyName("street_address1")]
        public string StreetAddress1 { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }
    }
}
