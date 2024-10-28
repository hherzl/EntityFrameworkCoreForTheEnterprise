using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
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
}
