using System.Collections.ObjectModel;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class Customer : AuditableEntity
    {
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
        public Country CountryFk { get; set; }

        public virtual Collection<PaymentTransaction> PaymentTransactionList { get; set; }
    }
}
