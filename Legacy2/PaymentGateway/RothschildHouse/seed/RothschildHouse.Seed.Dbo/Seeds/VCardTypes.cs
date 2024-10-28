using RothschildHouse.Domain.Entities;
using RothschildHouse.Domain.Enums;

namespace RothschildHouse.Seed.Dbo.Seeds;

internal class VCardTypes
{
    public static IEnumerable<EnumDescription> Items
    {
        get
        {
            yield return new EnumDescription
            {
                Type = typeof(CardType).FullName,
                Value = (short)CardType.Debit,
                Description = "Debit"
            };

            yield return new EnumDescription
            {
                Type = typeof(CardType).FullName,
                Value = (short)CardType.Credit,
                Description = "Credit"
            };
        }
    }
}
