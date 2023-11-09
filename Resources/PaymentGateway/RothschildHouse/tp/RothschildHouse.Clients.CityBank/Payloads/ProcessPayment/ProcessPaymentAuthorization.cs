namespace RothschildHouse.Clients.CityBank.Payloads.ProcessPayment;

public record ProcessPaymentAuthorization
{
    public string TransactionId { get; set; }
    public string TransactionCode { get; set; }
    public string TransactionPayload { get; set; }
}
