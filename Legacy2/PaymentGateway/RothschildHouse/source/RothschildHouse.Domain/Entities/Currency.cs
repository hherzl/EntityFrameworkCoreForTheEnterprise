using System.Collections.ObjectModel;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Domain.Entities;

public partial class Currency : AuditableEntity
{
    public short? Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public decimal? Rate { get; set; }

    public virtual Collection<Transaction> TransactionList { get; set; }
}
