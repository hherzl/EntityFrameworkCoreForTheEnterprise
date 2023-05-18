using System.Collections.ObjectModel;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class Person : AuditableEntity
    {
        public int? Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }

        public virtual Collection<Customer> CustomerList { get; set; }
    }
}
