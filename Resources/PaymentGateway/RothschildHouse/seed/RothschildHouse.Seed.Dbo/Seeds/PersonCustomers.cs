using RothschildHouse.Domain.Entities;

namespace RothschildHouse.Seed.Dbo.Seeds;

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
                CountryId = 1,
                Phone = "+150377778888",
                Email = "juan.perez@gmail.com",
                AlienGuid = Guid.Parse("867D280C-8BFA-4ACB-B64E-76BAAD10B63D")
            });

            yield return (new Person
            {
                GivenName = "María",
                FamilyName = "Rosales",
                FullName = "María Rosales"
            },
            new Customer
            {
                Id = Guid.Parse("2341BD82-0235-4233-A103-24CC8D83A51A"),
                CountryId = 1,
                Phone = "+150366665555",
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
                CountryId = 1,
                Phone = "+150377889900",
                Email = "julioc.bonilla2000@gmail.com"
            });

            yield return (new Person
            {
                GivenName = "Roberto",
                FamilyName = "Asturias",
                FullName = "Roberto Asturias"
            },
            new Customer
            {
                Id = Guid.Parse("F2E95CCA-6E96-456C-92B1-E016990458FF"),
                CountryId = 1,
                Phone = "+150366558877",
                Email = "roberto_asturias_sv@hotmail.com"
            });

            yield return (new Person
            {
                GivenName = "Amelia",
                FamilyName = "Brand",
                FullName = "Amelia Brand"
            },
            new Customer
            {
                Id = Guid.Parse("4B76BB9C-BF57-43C7-B75E-13EB0C047F47"),
                CountryId = 1,
                Phone = "+170211223344",
                Email = "ameliabrand@nasa.gov"
            });

            yield return (new Person
            {
                GivenName = "Joseph",
                FamilyName = "Cooper",
                FullName = "Joseph Cooper"
            },
            new Customer
            {
                Id = Guid.Parse("EE3EE5D7-40A1-4AA8-9B8D-B596B6B8C575"),
                CountryId = 1,
                Phone = "+170222334455",
                Email = "joseph.cooper@farmersunion.us"
            });
        }
    }
}
