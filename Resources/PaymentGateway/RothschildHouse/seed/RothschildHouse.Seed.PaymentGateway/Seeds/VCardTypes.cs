using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Domain.Core.Enums;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
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
}
