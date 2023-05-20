using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class PaymentTransactionLog : AuditableEntity
    {
        public const string ApplicationJson = "application/json";

        public long? Id { get; set; }
        public long? PaymentTransactionId { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string LogType { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }

        public virtual PaymentTransaction PaymentTransactionFk { get; set; }
    }
}
