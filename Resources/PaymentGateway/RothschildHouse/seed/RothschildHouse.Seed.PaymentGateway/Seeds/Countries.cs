using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
    internal class Countries
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
}
