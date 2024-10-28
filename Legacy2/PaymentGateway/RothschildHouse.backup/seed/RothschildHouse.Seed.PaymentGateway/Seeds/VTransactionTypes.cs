using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Domain.Core.Enums;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
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
}
