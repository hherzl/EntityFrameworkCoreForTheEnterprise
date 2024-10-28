using System.Collections.ObjectModel;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Domain.Entities;

public partial class Country : AuditableEntity
{
    public short? Id { get; set; }
    public string Name { get; set; }
    public string TwoLetterIsoCode { get; set; }
    public string ThreeLetterIsoCode { get; set; }

    public virtual Collection<Customer> CustomerList { get; set; }
}
