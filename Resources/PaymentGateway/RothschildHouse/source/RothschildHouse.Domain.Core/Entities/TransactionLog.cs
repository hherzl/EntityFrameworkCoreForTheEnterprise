using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class TransactionLog : AuditableEntity
    {
        public long? Id { get; set; }
        public long? TransactionId { get; set; }
        public short? TransactionStatusId { get; set; }
        public string LogType { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }

        public virtual Transaction TransactionFk { get; set; }
    }
}
