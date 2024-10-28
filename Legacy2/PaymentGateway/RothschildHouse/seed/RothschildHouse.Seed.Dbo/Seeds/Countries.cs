using RothschildHouse.Domain.Entities;

namespace RothschildHouse.Seed.Dbo.Seeds;

internal static class Countries
{
    public static IEnumerable<Country> Items
    {
        get
        {
            yield return new Country
            {
                Name = "United States of America",
                TwoLetterIsoCode = "US",
                ThreeLetterIsoCode = "USA"
            };
        }
    }
}
