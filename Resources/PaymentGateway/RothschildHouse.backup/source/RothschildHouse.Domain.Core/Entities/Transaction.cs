using System.Collections.ObjectModel;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class Transaction : AuditableEntity
    {
        public long? Id { get; set; }
        public Guid? Guid { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public short? TransactionTypeId { get; set; }
        public short? TransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientFullClassName { get; set; }
        public Guid? CustomerId { get; set; }
        public int? StoreId { get; set; }
        public Guid? CardId { get; set; }
        public decimal? Amount { get; set; }
        public short? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string Notes { get; set; }

        public virtual ClientApplication ClientApplicationFk { get; set; }
        public virtual Customer CustomerFk { get; set; }
        public virtual Card CardFk { get; set; }
        public virtual Currency CurrencyFk { get; set; }
        public virtual Collection<TransactionLog> TransactionLogList { get; set; }
    }
}
