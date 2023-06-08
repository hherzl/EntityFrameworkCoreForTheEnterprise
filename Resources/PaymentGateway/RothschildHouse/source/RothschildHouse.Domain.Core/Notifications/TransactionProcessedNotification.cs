using MediatR;

namespace RothschildHouse.Domain.Core.Notifications
{
    public class TransactionProcessedNotification : INotification
    {
        public long? Id { get; set; }
        public Guid? Guid { get; set; }
        public string ClientApplication { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }
}
