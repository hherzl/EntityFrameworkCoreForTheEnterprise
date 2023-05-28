using System.Text.Json;

namespace RothschildHouse.TP.CityBank.Payloads.Cvv2
{
    internal class Cvv2Payload
    {
        public string Cvv2ResultCode { get; set; }

        public string ToJson()
            => JsonSerializer.Serialize(this, GlobalJsonSerializerOptions.Default);
    }
}
