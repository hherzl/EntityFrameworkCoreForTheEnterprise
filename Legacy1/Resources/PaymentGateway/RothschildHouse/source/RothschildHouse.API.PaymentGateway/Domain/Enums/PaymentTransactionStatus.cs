namespace RothschildHouse.API.PaymentGateway.Domain.Enums
{
#pragma warning disable CS1591
    public enum PaymentTransactionStatus : short
    {
        Requested = 0,
        Denied = 5,
        Processed = 10
    }
}
