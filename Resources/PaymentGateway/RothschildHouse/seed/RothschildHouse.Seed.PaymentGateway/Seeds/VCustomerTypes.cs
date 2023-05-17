using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Domain.Enums;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
    internal class VCustomerTypes
    {
        public static IEnumerable<EnumDescription> Items
        {
            get
            {
                yield return new EnumDescription
                {
                    Type = typeof(CustomerType).FullName,
                    Value = (short)CustomerType.Person,
                    Description = "Person"
                };

                yield return new EnumDescription
                {
                    Type = typeof(CustomerType).FullName,
                    Value = (short)CustomerType.Company,
                    Description = "Company"
                };
            }
        }
    }
}
