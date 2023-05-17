using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class PaymentTransactionLog : AuditableEntity
    {
        public const string ApplicationJson = "application/json";

        public PaymentTransactionLog()
        {
        }

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
