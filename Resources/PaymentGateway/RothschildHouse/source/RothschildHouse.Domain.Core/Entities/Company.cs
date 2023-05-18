using System.Collections.ObjectModel;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Domain.Core.Entities
{
    public class Company : AuditableEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public virtual Collection<Customer> CustomerList { get; set; }
    }
}
