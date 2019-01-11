using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var charlesXavier = new Person
            {
                PersonID = Guid.Parse("45783940-5124-46F9-B54F-5A2149F35117"),
                GivenName = "Charles",
                MiddleName = "Francis",
                FamilyName = "Xavier",
                FullName = "Charles F Xavier",
                BirthDate = DateTime.Now
            };

            dbContext.People.Add(charlesXavier);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = charlesXavier.PersonID,
                CardHolderName = "Charles F Xavier",
                IssuingNetwork = "Visa",
                CardNumber = "4024007164051145",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 5, DateTime.Now.Month, 1),
                Cvv = "987",
                Limit = 10000,
                AvailableFounds = 10000
            });

            var erickLehnsherr = new Person
            {
                PersonID = Guid.Parse("F393026E-423A-4A1F-A343-4DB66C1FC8DA"),
                GivenName = "Erik",
                MiddleName = "Magnus",
                FamilyName = "Lehnsherr",
                FullName = "Erik Magnus Lehnsherr",
                BirthDate = DateTime.Now
            };

            dbContext.People.Add(erickLehnsherr);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = erickLehnsherr.PersonID,
                CardHolderName = "Erik M Lehnsherr",
                IssuingNetwork = "American Express",
                CardNumber = "347208491189735",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 6, DateTime.Now.Month + 2, 1),
                Cvv = "4321",
                Limit = 5000,
                AvailableFounds = 5000
            });

            dbContext.SaveChanges();
        }
    }
#pragma warning restore CS1591
}
