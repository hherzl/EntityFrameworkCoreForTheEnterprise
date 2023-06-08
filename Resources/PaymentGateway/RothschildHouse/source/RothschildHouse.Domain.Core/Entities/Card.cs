using System.Collections.ObjectModel;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class Card : AuditableEntity
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public virtual Collection<Transaction> TransactionList { get; set; }
    }
}
