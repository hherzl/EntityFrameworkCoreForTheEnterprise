using System;
using System.Collections.Generic;
using System.Linq;

namespace RothschildHouse.Models
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
            var jamesLogan = new Person
            {
                PersonID = Guid.NewGuid(),
                GivenName = "James",
                MiddleName = "",
                FamilyName = "Logan",
                FullName = "James Logan"
            };

            dbContext.People.Add(jamesLogan);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = jamesLogan.PersonID,
                CardHolderName = "James Logan",
                IssuingNetwork = "Visa",
                CardNumber = "4024007164051145",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 5, DateTime.Now.Month, 1),
                Cvv = "987",
                Limit = 10000,
                AvailableFounds = 10000
            });

            dbContext.SaveChanges();
        }
    }
#pragma warning restore CS1591
}
