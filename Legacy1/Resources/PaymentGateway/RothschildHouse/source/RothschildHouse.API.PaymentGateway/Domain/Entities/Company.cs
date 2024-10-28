using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Company : AuditableEntity
    {
        public Company()
        {
        }

        public int? Id { get; set; }
        public string Name { get; set; }

        public virtual Collection<Customer> CustomerList { get; set; }
    }
}
