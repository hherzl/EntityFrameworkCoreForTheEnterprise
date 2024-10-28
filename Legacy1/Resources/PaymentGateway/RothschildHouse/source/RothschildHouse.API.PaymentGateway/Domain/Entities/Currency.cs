using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Currency : AuditableEntity
    {
        public Currency()
        {
        }

        public short? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }

        public virtual Collection<PaymentTransaction> PaymentTransactionList { get; set; }
    }
}
