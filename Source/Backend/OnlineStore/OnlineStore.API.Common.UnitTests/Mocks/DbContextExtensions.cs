using System;
using OnlineStore.Core.Domain;
using OnlineStore.Core.Domain.Dbo;
using OnlineStore.Core.Domain.Warehouse;

namespace OnlineStore.API.Common.UnitTests.Mocks
{
    public static class DbContextExtensions
    {
        static string creationUser;
        static DateTime creationDateTime;

        static DbContextExtensions()
        {
            creationUser = "unittests";
            creationDateTime = DateTime.Now;
        }

        public static OnlineStoreDbContext SeedCountries(this OnlineStoreDbContext dbContext)
        {
            var country = new Country
            {
                ID = 1,
                CountryName = "USA",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Countries.Add(country);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedCurrencies(this OnlineStoreDbContext dbContext)
        {
            var currency = new Currency
            {
                ID = "USD",
                CurrencyName = "US Dollar",
                CurrencySymbol = "$",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Currencies.Add(currency);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedCountryCurrencies(this OnlineStoreDbContext dbContext)
        {
            var countryCurrency = new CountryCurrency
            {
                ID = 1,
                CountryID = 1,
                CurrencyID = "USD",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.CountryCurrencies.Add(countryCurrency);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProductCategories(this OnlineStoreDbContext dbContext)
        {
            var productCategory = new ProductCategory
            {
                ID = 1,
                ProductCategoryName = "PS4 Games",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductCategories.Add(productCategory);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProducts(this OnlineStoreDbContext dbContext)
        {
            var product = new Product
            {
                ID = 1,
                ProductName = "The King of Fighters XIV",
                ProductCategoryID = 1,
                UnitPrice = 29.99m,
                Description = "KOF XIV",
                Discontinued = false,
                Stocks = 15000,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Products.Add(product);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedLocations(this OnlineStoreDbContext dbContext)
        {
            var location = new Location
            {
                ID = "W01",
                LocationName = "Warehouse 01",
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.Locations.Add(location);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static OnlineStoreDbContext SeedProductInventories(this OnlineStoreDbContext dbContext)
        {
            var productInventory = new ProductInventory
            {
                ProductID = 1,
                LocationID = "W01",
                OrderDetailID = 1,
                Quantity = 1500,
                CreationUser = creationUser,
                CreationDateTime = creationDateTime
            };

            dbContext.ProductInventories.Add(productInventory);

            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
