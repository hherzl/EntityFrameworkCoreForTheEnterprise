using RothschildHouse.API.PaymentGateway.Domain.Entities;

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
