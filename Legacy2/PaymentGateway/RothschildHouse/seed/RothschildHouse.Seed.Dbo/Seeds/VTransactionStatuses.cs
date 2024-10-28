using RothschildHouse.Domain.Entities;
using RothschildHouse.Domain.Enums;

namespace RothschildHouse.Seed.Dbo.Seeds;

internal class VTransactionStatuses
{
    public static IEnumerable<EnumDescription> Items
    {
        get
        {
            yield return new EnumDescription
            {
                Type = typeof(TransactionStatus).FullName,
                Value = (short)TransactionStatus.Requested,
                Description = "Requested"
            };

            yield return new EnumDescription
            {
                Type = typeof(TransactionStatus).FullName,
                Value = (short)TransactionStatus.Denied,
                Description = "Denied"
            };

            yield return new EnumDescription
            {
                Type = typeof(TransactionStatus).FullName,
                Value = (short)TransactionStatus.Processed,
                Description = "Processed"
            };
        }
    }
}
