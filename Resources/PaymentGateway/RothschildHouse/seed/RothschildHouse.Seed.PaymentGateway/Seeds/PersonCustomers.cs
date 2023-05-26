using RothschildHouse.Domain.Core.Entities;

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

                yield return (new Person
                {
                    GivenName = "María",
                    MiddleName = "",
                    FamilyName = "Rosales",
                    FullName = "María Rosales"
                },
                new Customer
                {
                    Id = Guid.Parse("2341BD82-0235-4233-A103-24CC8D83A51A"),
                    Phone = "+50366665555",
                    Email = "maria.rosales@outlook.com"
                });

                yield return (new Person
                {
                    GivenName = "Julio",
                    MiddleName = "César",
                    FamilyName = "Bonilla",
                    FullName = "Julio César Bonilla"
                },
                new Customer
                {
                    Id = Guid.Parse("24868311-7D35-46B3-A6C7-57D10DE064C4"),
                    Phone = "+50377889900",
                    Email = "julioc.bonilla2000@gmail.com"
                });

                yield return (new Person
                {
                    GivenName = "Roberto",
                    MiddleName = "",
                    FamilyName = "Asturias",
                    FullName = "Roberto Asturias"
                },
                new Customer
                {
                    Id = Guid.Parse("F2E95CCA-6E96-456C-92B1-E016990458FF"),
                    Phone = "+50366558877",
                    Email = "roberto_asturias_sv@hotmail.com"
                });
            }
        }
    }
}
