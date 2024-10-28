namespace RothschildHouse.Clients.CityBank.Payloads.Cvv2.Mocks;

internal class Cvv2Mocks
{
    public static Cvv2Payload Match()
        => new()
        {
            Cvv2ResultCode = "M"
        };
}
