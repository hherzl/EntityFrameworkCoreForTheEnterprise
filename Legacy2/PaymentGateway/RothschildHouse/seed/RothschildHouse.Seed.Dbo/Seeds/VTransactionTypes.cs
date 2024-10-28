using RothschildHouse.Domain.Entities;
using RothschildHouse.Domain.Enums;

namespace RothschildHouse.Seed.Dbo.Seeds;

internal class VTransactionTypes
{
    public static IEnumerable<EnumDescription> Items
    {
        get
        {
            yield return new EnumDescription
            {
                Type = typeof(TransactionType).FullName,
                Value = (short)TransactionType.Payment,
                Description = "Payment"
            };
        }
    }
}
