using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Application.Core.Features.Cards.Queries;
using RothschildHouse.Application.Core.Features.Customers.Queries;
using RothschildHouse.Application.Core.Features.PaymentTransactions.Queries;
using RothschildHouse.Domain.Core.Entities;

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

        public static IQueryable<CardItemModel> GetCards(this IRothschildHouseDbContext ctx)
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
                    CardNumber = card.CardNumber,
                    ExpirationDate = card.ExpirationDate
                };

            return query;
        }

        public static async Task<Card> GetCardAsync(this IRothschildHouseDbContext ctx, Guid? id, bool tracking = true, bool include = true, CancellationToken cancellationToken = default)
        {
            var query = ctx.Card.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }

        public static async Task<Card> GetCardAsync(this IRothschildHouseDbContext ctx, string issuingNetwork, string cardNumber, bool tracking = true, bool include = true, CancellationToken cancellationToken = default)
        {
            var query = ctx.Card.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(item => item.IssuingNetwork == issuingNetwork && item.CardNumber == cardNumber, cancellationToken);
        }

        public static IQueryable<CustomerItemModel> GetCustomers(this IRothschildHouseDbContext ctx)
        {
            var query =
                from customer in ctx.Customer
                join person in ctx.Person on customer.PersonId equals person.Id into customerPersonGroup
                from customerPerson in customerPersonGroup.DefaultIfEmpty()
                join company in ctx.Company on customer.CompanyId equals company.Id into customerCompanyGroup
                from customerCompany in customerCompanyGroup.DefaultIfEmpty()
                join country in ctx.Company on customer.CountryId equals country.Id into customerCountryGroup
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

            return query;
        }

        public static async Task<Customer> GetCustomerAsync(this IRothschildHouseDbContext ctx, Guid? id, bool tracking = true, bool include = true, CancellationToken cancellationToken = default)
        {
            var query = ctx.Customer.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            if (include)
                query = query.Include(e => e.PersonFk).Include(e => e.CompanyFk);

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }

        public static async Task<Customer> GetCustomerByAlienGuidAsync(this IRothschildHouseDbContext ctx, Guid? guid, bool tracking = true, bool include = true, CancellationToken cancellationToken = default)
        {
            var query = ctx.Customer.AsQueryable();

            if (tracking == false)
                query = query.AsNoTracking();

            if (include)
                query = query.Include(e => e.PersonFk).Include(e => e.CompanyFk);

            return await query.FirstOrDefaultAsync(item => item.AlienGuid == guid, cancellationToken);
        }

        public static IQueryable<PaymentTransactionItemModel> GetPaymentTransactions
        (
            this IRothschildHouseDbContext ctx,
            short? paymentTransactionStatusId = null,
            Guid? clientApplicationId = null,
            Guid? customerId = null,
            Guid? cardId = null,
            DateTime? startDate = null,
            DateTime? endDate = null
        )
        {
            var query =
                from txn in ctx.PaymentTransaction
                join paymentTransactionStatus in ctx.VPaymentTransactionStatus on txn.PaymentTransactionStatusId equals paymentTransactionStatus.Id
                join clientApplication in ctx.ClientApplication on txn.ClientApplicationId equals clientApplication.Id
                join currency in ctx.Currency on txn.CurrencyId equals currency.Id
                where txn.Active == true
                select new PaymentTransactionItemModel
                {
                    Id = txn.Id,
                    PaymentTransactionStatusId = txn.PaymentTransactionStatusId,
                    PaymentTransactionStatus = paymentTransactionStatus.Name,
                    ClientApplicationId = txn.ClientApplicationId,
                    ClientApplication = clientApplication.Name,
                    CardId = txn.CardId,
                    CustomerId = txn.CustomerId,
                    OrderTotal = txn.Amount,
                    Currency = currency.Code,
                    CreationDateTime = txn.CreationDateTime
                };

            if (paymentTransactionStatusId.HasValue)
                query = query.Where(item => item.PaymentTransactionStatusId == paymentTransactionStatusId);

            if (clientApplicationId.HasValue)
                query = query.Where(item => item.ClientApplicationId == clientApplicationId);

            if (customerId.HasValue)
                query = query.Where(item => item.CustomerId == customerId);

            if (cardId.HasValue)
                query = query.Where(item => item.CardId == cardId);

            if (startDate.HasValue)
                query = query.Where(item => item.CreationDateTime >= startDate.ToStartDateTime());

            if (endDate.HasValue)
                query = query.Where(item => item.CreationDateTime <= endDate.ToEndDateTime());

            return query;
        }

        public static async Task<PaymentTransaction> GetPaymentTransactionAsync(this IRothschildHouseDbContext ctx, long? id, bool tracking = true, bool include = true, CancellationToken cancellationToken = default)
        {
            var query = ctx.PaymentTransaction.AsQueryable();

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
                    .Include(e => e.PaymentTransactionLogList)
                    ;
            }

            return await query.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        }
    }
}
