using RothschildHouse.Domain.Entities;

namespace RothschildHouse.Seed.Dbo.Seeds;

internal class Currencies
{
    public static IEnumerable<Currency> Items
    {
        get
        {
            yield return new Currency
            {
                Name = "US Dollar",
                Code = "USD",
                Rate = 1
            };
        }
    }
}
