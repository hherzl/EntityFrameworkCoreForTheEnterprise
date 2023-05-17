using RothschildHouse.API.PaymentGateway.Domain.Entities;

namespace RothschildHouse.Seed.PaymentGateway.Seeds
{
    internal class PersonCustomers
    {
        public static IEnumerable<(Person, Customer)> Items
        {
            get
            {
                yield return (new Person
                {
                    GivenName = "Juan",
                    MiddleName = "Carlos",
                    FamilyName = "Pérez",
                    FullName = "Juan Carlos Pérez"
                },
                new Customer
                {
                    Id = Guid.Parse("811A6254-F551-4FEB-AC9B-EC934E024BEC"),
                    Phone = "+50377778888",
                    Email = "juan.perez@gmail.com",
                    AlienGuid = Guid.Parse("867D280C-8BFA-4ACB-B64E-76BAAD10B63D")
                });
            }
        }
    }
}
