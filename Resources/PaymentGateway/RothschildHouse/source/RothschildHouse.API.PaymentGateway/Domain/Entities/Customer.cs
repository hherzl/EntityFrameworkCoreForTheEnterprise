using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Customer : AuditableEntity
    {
        public Customer()
        {
        }

        public Guid? Id { get; set; }
        public int? PersonId { get; set; }
        public int? CompanyId { get; set; }
        public short? CountryId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }

        public Person PersonFk { get; set; }
        public Company CompanyFk { get; set; }
        public virtual Collection<PaymentTransaction> PaymentTransactionList { get; set; }
    }
}
