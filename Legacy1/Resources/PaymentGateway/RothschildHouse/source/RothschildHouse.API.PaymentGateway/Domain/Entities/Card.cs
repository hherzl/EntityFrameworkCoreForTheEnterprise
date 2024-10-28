using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Card : AuditableEntity
    {
        public Card()
        {
        }

        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public virtual Collection<PaymentTransaction> PaymentTransactionList { get; set; }
    }
}
