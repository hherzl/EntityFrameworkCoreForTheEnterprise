using System;
using System.Collections.Generic;
using System.Linq;

namespace RothschildHouse.Models
{
    public static class DomainDrivenDesignExtensions
    {
        public static Person GetPersonByFullName(this PaymentDbContext dbContext, string fullName)
            => dbContext.People.FirstOrDefault(item => item.FullName.ToLower() == fullName.ToLower());

        public static IEnumerable<CreditCard> GetCreditCardsByPersonID(this PaymentDbContext dbContext, Guid personID)
            => dbContext.CreditCards.Where(item => item.PersonID == personID);
    }
}
