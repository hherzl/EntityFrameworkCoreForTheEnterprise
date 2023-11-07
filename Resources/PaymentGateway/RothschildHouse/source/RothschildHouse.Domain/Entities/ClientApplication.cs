using System.Collections.ObjectModel;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Domain.Entities;

public partial class ClientApplication : AuditableEntity
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public virtual Collection<Transaction> TransactionList { get; set; }
}
