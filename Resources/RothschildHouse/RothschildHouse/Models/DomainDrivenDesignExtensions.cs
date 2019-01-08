using System;
using System.Collections.Generic;
using System.Linq;

namespace RothschildHouse.Models
{
#pragma warning disable CS1591
    public static class DomainDrivenDesignExtensions
    {
        public static Person GetPersonByFullName(this PaymentDbContext dbContext, string fullName)
            => dbContext.People.FirstOrDefault(item => item.FullName.ToLower() == fullName.ToLower());

        public static IEnumerable<CreditCard> GetCreditCardsByPersonID(this PaymentDbContext dbContext, Guid personID)
            => dbContext.CreditCards.Where(item => item.PersonID == personID);

        public static void SeedInMemory(this PaymentDbContext dbContext)
        {
            var charlesXavier = new Person
            {
                PersonID = Guid.Parse("45783940-5124-46F9-B54F-5A2149F35117"),
                GivenName = "Charles",
                MiddleName = "F",
                FamilyName = "Xavier",
                FullName = "Charles F Xavier",
                BirthDate = DateTime.Now
            };

            dbContext.People.Add(charlesXavier);

            dbContext.CreditCards.Add(new CreditCard
            {
                CreditCardID = Guid.NewGuid(),
                PersonID = charlesXavier.PersonID,
                IssuingNetwork = "Visa",
                CardNumber = "4024007164051145",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 5, DateTime.Now.Month, 1),
                Cvv = "987"
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
                IssuingNetwork = "American Express",
                CardNumber = "347208491189735",
                Last4Digits = "1145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 6, DateTime.Now.Month + 2, 1),
                Cvv = "4321"
            });
            
            dbContext.SaveChanges();
        }
    }
#pragma warning restore CS1591
}
