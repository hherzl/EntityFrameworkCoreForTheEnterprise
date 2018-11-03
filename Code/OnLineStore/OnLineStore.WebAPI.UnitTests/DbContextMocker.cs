using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.WebAPI.UnitTests
{
    public static class DbContextMocker
    {
        public static StoreDbContext GetStoreDbContextInMemory(string dbName)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new StoreDbContext(options);

            dbContext.SeedInMemory();

            return dbContext;
        }
    }
}
