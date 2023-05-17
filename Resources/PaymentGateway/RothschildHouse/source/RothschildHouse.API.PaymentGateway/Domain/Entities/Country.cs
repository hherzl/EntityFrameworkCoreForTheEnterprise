using RothschildHouse.API.PaymentGateway.Domain.Common;
using System.Collections.ObjectModel;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class Country : AuditableEntity
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }

        public virtual Collection<Customer> CustomerList { get; set; }
    }
}
