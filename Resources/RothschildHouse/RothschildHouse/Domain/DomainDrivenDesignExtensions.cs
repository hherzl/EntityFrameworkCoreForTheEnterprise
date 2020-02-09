using System;
using System.Collections.Generic;
using System.Linq;

namespace RothschildHouse.Domain
{
#pragma warning disable CS1591
    public static class DomainDrivenDesignExtensions
    {
        public static IQueryable<Person> GetPersonsByFullName(this PaymentDbContext dbContext, string givenName, string middleName, string familyName)
        {
            var query = dbContext.People.AsQueryable();

            if (!string.IsNullOrEmpty(givenName))
                query = query.Where(item => item.FullName.ToLower().Contains(givenName.ToLower()));

            if (!string.IsNullOrEmpty(middleName))
                query = query.Where(item => item.FullName.ToLower().Contains(middleName.ToLower()));

            if (!string.IsNullOrEmpty(familyName))
                query = query.Where(item => item.FullName.ToLower().Contains(familyName.ToLower()));

            return query;
        }

        public static IQueryable<CreditCard> GetCreditCardByCardHolderName(this PaymentDbContext dbContext, string fullName)
            => dbContext.CreditCards.Where(item => item.CardHolderName.ToLower() == fullName.ToLower());

        public static IEnumerable<CreditCard> GetCreditCardsByPersonID(this PaymentDbContext dbContext, Guid personID)
            => dbContext.CreditCards.Where(item => item.PersonID == personID);

        public static void SeedInMemory(this PaymentDbContext dbContext)
        {
            dbContext.PaymentMethods.Add(new PaymentMethod
            {
                PaymentMethodID = Guid.Parse("7671A4F7-A735-4CB7-AAB4-CF47AE20171D"),
                PaymentMethodName = "Credit Card"
            });

            /* Get more credit card numbers: http://www.getcreditcardnumbers.com/ */

            var wolverine = new Person
            {
                PersonID = Guid.NewGuid(),
                GivenName = "James",
                MiddleName = "",
                FamilyName = "Logan",
                FullName = "James Logan"
            };

            dbContext.People.Add(wolverine);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = wolverine.PersonID,
                CardHolderName = "James Logan",
                IssuingNetwork = "Visa",
                CardNumber = "4024007164051145",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(2024, 6, 1),
                Cvv = "987",
                Limit = 10000,
                AvailableFounds = 10000
            });

            var storm = new Person
            {
                PersonID = Guid.NewGuid(),
                GivenName = "Ororo",
                MiddleName = "",
                FamilyName = "Munroe",
                FullName = "Ororo Munroe"
            };

            dbContext.People.Add(storm);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = storm.PersonID,
                CardHolderName = "Ororo Munroe",
                IssuingNetwork = "MasterCard",
                CardNumber = "5473913699329307",
                Last4Digits = "9307",
                ExpirationDate = new DateTime(2023, 1, 1),
                Cvv = "987",
                Limit = 5000,
                AvailableFounds = 5000
            });

            dbContext.SaveChanges();
        }
    }
#pragma warning restore CS1591
}
