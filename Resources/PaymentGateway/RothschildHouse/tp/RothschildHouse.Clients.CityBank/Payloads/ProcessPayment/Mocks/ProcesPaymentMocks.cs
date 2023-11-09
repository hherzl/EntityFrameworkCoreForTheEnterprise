namespace RothschildHouse.Clients.CityBank.Payloads.ProcessPayment.Mocks;

internal class ProcessPaymentMocks
{
    public static ProcessPaymentAuthorization MockAuthorization()
        => new()
        {
            TransactionId = Guid.NewGuid().ToString(),
            TransactionCode = TimeSpan.FromDays(DateTime.Now.TimeOfDay.TotalDays).ToString(),
            TransactionPayload = "{ }"
        };

    public static ProcessPaymentCapture MockCapture()
        => new()
        {
            TransactionId = Guid.NewGuid().ToString(),
            TransactionPayload = "{ }"
        };
}
