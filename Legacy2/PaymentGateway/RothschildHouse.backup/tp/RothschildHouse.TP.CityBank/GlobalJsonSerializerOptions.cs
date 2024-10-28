using System.Text.Json;

namespace RothschildHouse.TP.CityBank
{
    internal class GlobalJsonSerializerOptions
    {
        public static JsonSerializerOptions Default
            => new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
    }
}
