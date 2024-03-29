﻿using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Library.Common.Clients.Models.PaymentGateway;

namespace RothschildHouse.Application.Core
{
    public static class IRothschildHouseDbContextQueries
    {
        public static async Task<Country> GetCountryAsync(this IRothschildHouseDbContext ctx, short? id)
            => await ctx.Country.FirstOrDefaultAsync(item => item.Id == id);

        public static async Task<Country> GetCountryByTwoLetterIsoCodeAsync(this IRothschildHouseDbContext ctx, string twoLetterIsoCode)
            => await ctx.Country.FirstOrDefaultAsync(item => item.TwoLetterIsoCode == twoLetterIsoCode);

        public static async Task<Currency> GetCurrencyAsync(this IRothschildHouseDbContext ctx, short? id)
            => await ctx.Currency.FirstOrDefaultAsync(item => item.Id == id);

        public static async Task<Currency> GetCurrencyAsync(this IRothschildHouseDbContext ctx, string code)
            => await ctx.Currency.FirstOrDefaultAsync(item => item.Code == code);

        public static async Task<ClientApplication> GetClientApplicationAsync(this IRothschildHouseDbContext ctx, Guid? id)
            => await ctx.ClientApplication.FirstOrDefaultAsync(item => item.Id == id);

        public static IQueryable<CardItemModel> GetCards(this IRothschildHouseDbContext ctx, long? cardTypeId = null, string issuingNetwork = null, string cardholderName = null)
        {
            var query =
                from card in ctx.Card
                join cardType in ctx.VCardType on card.CardTypeId equals cardType.Id
                where card.Active == true
                select new CardItemModel
                {
                    Id = card.Id,
                    CardTypeId = card.CardTypeId,
                    CardType = cardType.Name,
                    IssuingNetwork = card.IssuingNetwork,
                    CardholderName = card.CardholderName,
                    Last4Digits = card.CardNumber,
                    ExpirationDate = card.ExpirationDate
                };

            if (cardTypeId.HasValue)
                query = query.Where(item => item.CardTypeId == cardTypeId);

            if (!string.IsNullOrEmpty(issuingNetwork))
                query = query.Where(item => item.IssuingNetwork.Contains(issuingNetwork));

            if (!string.IsNullOrEmpty(cardholderName))
                query = query.Where(item => item.CardholderName.Contains(cardholderName));

            return query;
        }

        public static async Task<Card> GetCardAsync(this IRothschildHouseDbContext ctx, Guid? id, CancellationToken cancellationToken = default, bool tracking = true)
        {
            var query = ctx.Card.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }

        public static async Task<Card> GetCardByAsync(this IRothschildHouseDbContext ctx, string issuingNetwork, string cardNumber, CancellationToken cancellationToken = default, bool tracking = true)
        {
            var query = ctx.Card.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(item => item.IssuingNetwork == issuingNetwork && item.CardNumber == cardNumber, cancellationToken);
        }

        public static IQueryable<CustomerItemModel> GetCustomers(this IRothschildHouseDbContext ctx, string fullName = null, short? countryId = null, string phone = null, string email = null)
        {
            var query =
                from customer in ctx.Customer
                join person in ctx.Person on customer.PersonId equals person.Id into customerPersonGroup
                from customerPerson in customerPersonGroup.DefaultIfEmpty()
                join company in ctx.Company on customer.CompanyId equals company.Id into customerCompanyGroup
                from customerCompany in customerCompanyGroup.DefaultIfEmpty()
                join country in ctx.Country on customer.CountryId equals country.Id into customerCountryGroup
                from customerCountry in customerCountryGroup.DefaultIfEmpty()
                where customer.Active == true
                select new CustomerItemModel
                {
                    Id = customer.Id,
                    FullName = customerPerson.FullName,
                    Name = customerCompany.Name,
                    CountryId = customer.CountryId,
                    Country = customerCountry.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    AlienGuid = customer.AlienGuid
                };

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(item => item.FullName.Contains(fullName));

            if (countryId.HasValue)
                query = query.Where(item => item.CountryId == countryId);

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(item => item.Phone.Contains(phone));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(item => item.Email.Contains(email));

            return query;
        }

        public static async Task<Customer> GetCustomerAsync(this IRothschildHouseDbContext ctx, Guid? id, CancellationToken cancellationToken = default, bool tracking = true, bool include = true)
        {
            var query = ctx.Customer.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            if (include)
            {
                query = query
                    .Include(e => e.PersonFk)
                    .Include(e => e.CompanyFk)
                    .Include(e => e.CountryFk)
                    ;
            }

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }

        public static async Task<Customer> GetCustomerByAlienGuidAsync(this IRothschildHouseDbContext ctx, Guid? guid, CancellationToken cancellationToken = default, bool tracking = true, bool include = true)
        {
            var query = ctx.Customer.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            if (include)
            {
                query = query
                    .Include(e => e.PersonFk)
                    .Include(e => e.CompanyFk)
                    .Include(e => e.CountryFk)
                    ;
            }

            return await query.FirstOrDefaultAsync(item => item.AlienGuid == guid, cancellationToken);
        }

        public static IQueryable<TransactionItemModel> GetTransactions
        (
            this IRothschildHouseDbContext ctx,
            short? transactionStatusId = null,
            Guid? clientApplicationId = null,
            Guid? customerId = null,
            Guid? cardId = null,
            DateTime? startDate = null,
            DateTime? endDate = null
        )
        {
            var query =
                from txn in ctx.Transaction
                join txnType in ctx.VTransactionType on txn.TransactionTypeId equals txnType.Id
                join txnStatus in ctx.VTransactionStatus on txn.TransactionStatusId equals txnStatus.Id
                join clientApplication in ctx.ClientApplication on txn.ClientApplicationId equals clientApplication.Id
                join card in ctx.Card on txn.CardId equals card.Id
                join currency in ctx.Currency on txn.CurrencyId equals currency.Id
                where txn.Active == true
                select new TransactionItemModel
                {
                    Id = txn.Id,
                    TransactionDateTime = txn.TransactionDateTime,
                    TransactionTypeId = txn.TransactionTypeId,
                    TransactionType = txnType.Name,
                    TransactionStatusId = txn.TransactionStatusId,
                    TransactionStatus = txnStatus.Name,
                    ClientApplicationId = txn.ClientApplicationId,
                    ClientApplication = clientApplication.Name,
                    CardId = txn.CardId,
                    IssuingNetwork = card.IssuingNetwork,
                    CardNumber = card.CardNumber,
                    CustomerId = txn.CustomerId,
                    OrderTotal = txn.Amount,
                    Currency = currency.Code,
                    CreationDateTime = txn.CreationDateTime
                };

            if (transactionStatusId.HasValue)
                query = query.Where(item => item.TransactionStatusId == transactionStatusId);

            if (clientApplicationId.HasValue)
                query = query.Where(item => item.ClientApplicationId == clientApplicationId);

            if (customerId.HasValue)
                query = query.Where(item => item.CustomerId == customerId);

            if (cardId.HasValue)
                query = query.Where(item => item.CardId == cardId);

            if (startDate.HasValue)
                query = query.Where(item => item.TransactionDateTime >= startDate.ToStartDateTime());

            if (endDate.HasValue)
                query = query.Where(item => item.TransactionDateTime <= endDate.ToEndDateTime());

            return query;
        }

        public static async Task<Transaction> GetTransactionAsync(this IRothschildHouseDbContext ctx, long? id, CancellationToken cancellationToken = default, bool tracking = true, bool include = true)
        {
            var query = ctx.Transaction.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            if (include)
            {
                query = query
                    .Include(e => e.ClientApplicationFk)
                    .Include(e => e.CustomerFk)
                        .ThenInclude(e => e.PersonFk)
                    .Include(e => e.CustomerFk)
                        .ThenInclude(e => e.CompanyFk)
                    .Include(e => e.CardFk)
                    .Include(e => e.CurrencyFk)
                    .Include(e => e.TransactionLogList)
                    ;
            }

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }
    }
}
