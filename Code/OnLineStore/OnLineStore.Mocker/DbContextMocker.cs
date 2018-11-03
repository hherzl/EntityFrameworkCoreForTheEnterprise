using Microsoft.EntityFrameworkCore;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.Mocker
{
    public static class DbContextMocker
    {
        private static readonly string ConnectionString;

        static DbContextMocker()
        {
            // todo: Load connection string from appsettings.json file
            ConnectionString = "server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;";
        }

        public static StoreDbContext GetStoreDbContext()
            => new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseSqlServer(ConnectionString).Options);
    }
}
