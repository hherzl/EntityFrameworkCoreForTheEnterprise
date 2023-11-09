using System.Text.Json;

namespace RothschildHouse.Application.Clients.PaymentGateway.ThirdParty;

internal class GlobalJsonSerializerOptions
{
    public static JsonSerializerOptions Default
        => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
}
