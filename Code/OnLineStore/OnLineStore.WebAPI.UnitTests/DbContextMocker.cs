using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.WebAPI.UnitTests
{
    public static class DbContextMocker
    {
        public static OnLineStoreDbContext GetStoreDbContextInMemory(string dbName)
        {
            var options = new DbContextOptionsBuilder<OnLineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var dbContext = new OnLineStoreDbContext(options);

            dbContext.SeedInMemory();

            return dbContext;
        }
    }
}
