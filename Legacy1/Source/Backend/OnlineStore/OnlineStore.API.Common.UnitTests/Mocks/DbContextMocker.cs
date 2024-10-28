﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineStore.Core.Domain;

namespace OnlineStore.API.Common.UnitTests.Mocks
{
    public static class DbContextMocker
    {
        public static OnlineStoreDbContext GetOnlineStoreDbContextInMemory(string dbName, bool seedWarehouseSchema = false, bool seedSalesSchema = false)
        {
            // Create options for DbContext
            // Use in memory provider
            // Disable transactions because in memory database doesn't support txns

            var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new OnlineStoreDbContext(options);

            // Seed data for DbContext instance

            dbContext
                .SeedCountries()
                .SeedCurrencies()
                .SeedCountryCurrencies()
            ;

            if (seedWarehouseSchema)
            {
                dbContext
                    .SeedProductCategories()
                    .SeedProducts()
                    .SeedLocations()
                    .SeedProductInventories()
                ;
            }

            if (seedSalesSchema)
            {

            }

            return dbContext;
        }
    }
}
