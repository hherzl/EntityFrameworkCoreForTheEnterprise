using System.Text.Json;

namespace RothschildHouse.TP.CityBank.Contracts.DataContracts
{
    public class ProcessPaymentRequest
    {
        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });
    }
}
