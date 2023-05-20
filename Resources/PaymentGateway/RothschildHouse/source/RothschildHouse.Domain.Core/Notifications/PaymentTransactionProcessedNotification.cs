using MediatR;

namespace RothschildHouse.Domain.Core.Notifications
{
    public class PaymentTransactionProcessedNotification : INotification
    {
        public Guid? Guid { get; set; }
        public string ClientApplication { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }
}
