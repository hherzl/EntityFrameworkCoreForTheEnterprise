using System.Collections.ObjectModel;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Domain.Entities;

public partial class Company : AuditableEntity
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public virtual Collection<Customer> CustomerList { get; set; }
}
