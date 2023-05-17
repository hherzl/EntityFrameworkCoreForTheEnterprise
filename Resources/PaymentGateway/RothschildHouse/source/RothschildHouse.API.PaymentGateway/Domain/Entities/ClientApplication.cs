using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class ClientApplication : AuditableEntity
    {
        public ClientApplication()
        {
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual Collection<PaymentTransaction> PaymentTransactionList { get; set; }
    }
}
