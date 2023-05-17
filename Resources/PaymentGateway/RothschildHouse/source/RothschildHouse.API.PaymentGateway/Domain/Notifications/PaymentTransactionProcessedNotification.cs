using MediatR;

namespace RothschildHouse.API.PaymentGateway.Domain.Notifications
{
#pragma warning disable CS1591
    public class PaymentTransactionProcessedNotification : INotification
    {
        public Guid? Guid { get; set; }
        public string ClientApplication { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }
}
