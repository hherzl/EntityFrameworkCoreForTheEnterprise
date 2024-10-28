using System.Text.Json;

namespace RothschildHouse.Clients.CityBank;

internal class GlobalJsonSerializerOptions
{
    public static JsonSerializerOptions Default
        => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
}
