using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
    internal class ClientApplications
    {
        public static IEnumerable<ClientApplication> Items
        {
            get
            {
                yield return new ClientApplication
                {
                    Id = Guid.Parse("D4159097-96BE-43E0-9E8F-ED4384B0F9C2"),
                    Name = "Rothschild House PayButton"
                };

                yield return new ClientApplication
                {
                    Id = Guid.Parse("9B26240D-6BFA-4F80-AE67-37712D1388A7"),
                    Name = "eCommerce client"
                };

                yield return new ClientApplication
                {
                    Id = Guid.Parse("B74CB3C2-BB35-4436-BCFB-8769B521CA3D"),
                    Name = "Mocks"
                };
            }
        }
    }
}
