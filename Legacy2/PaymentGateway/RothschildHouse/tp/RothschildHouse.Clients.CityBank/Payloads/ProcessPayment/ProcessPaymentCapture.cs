namespace RothschildHouse.Clients.CityBank.Payloads.ProcessPayment;

public record ProcessPaymentCapture
{
    public string TransactionId { get; set; }
    public string TransactionPayload { get; set; }
}
