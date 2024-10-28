using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Domain.Enums;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
    internal class VPaymentTransactionStatuses
    {
        public static IEnumerable<EnumDescription> Items
        {
            get
            {
                yield return new EnumDescription
                {
                    Type = typeof(PaymentTransactionStatus).FullName,
                    Value = (short)PaymentTransactionStatus.Requested,
                    Description = "Requested"
                };

                yield return new EnumDescription
                {
                    Type = typeof(PaymentTransactionStatus).FullName,
                    Value = (short)PaymentTransactionStatus.Denied,
                    Description = "Denied"
                };

                yield return new EnumDescription
                {
                    Type = typeof(PaymentTransactionStatus).FullName,
                    Value = (short)PaymentTransactionStatus.Processed,
                    Description = "Processed"
                };
            }
        }
    }
}
