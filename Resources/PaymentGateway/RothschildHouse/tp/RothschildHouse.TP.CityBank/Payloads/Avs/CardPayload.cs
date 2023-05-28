using System.Text.Json.Serialization;

namespace RothschildHouse.TP.CityBank.Payloads.Avs
{
    internal record CardPayload
    {
        [JsonPropertyName("card_number")]
        public string CardNumber { get; set; }

        [JsonPropertyName("expiry")]
        public string Expiry { get; set; }

        [JsonPropertyName("card_code")]
        public string CardCode { get; set; }
    }
}
