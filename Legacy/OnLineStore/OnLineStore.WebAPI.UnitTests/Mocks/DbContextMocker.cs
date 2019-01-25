using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.WebAPI.UnitTests.Mocks
{
    public static class DbContextMocker
    {
        public static OnLineStoreDbContext GetOnLineStoreDbContextInMemory(string dbName)
        {
            // Create options for DbContext
            // Use in memory provider
            // Disable transactions because in memory database doesn't support txns
            var options = new DbContextOptionsBuilder<OnLineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new OnLineStoreDbContext(options);

            // Seed data for DbContext instance
            dbContext.SeedInMemory();

            return dbContext;
        }
    }
}
