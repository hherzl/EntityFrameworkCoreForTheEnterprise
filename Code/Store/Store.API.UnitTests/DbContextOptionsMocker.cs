using Microsoft.EntityFrameworkCore;
using Store.Core.DataLayer;

namespace Store.API.UnitTests
{
    public static class DbContextOptionsMocker
    {
        public static DbContextOptions<StoreDbContext> GetDbContextOptions(string dbName)
            => new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
    }
}
