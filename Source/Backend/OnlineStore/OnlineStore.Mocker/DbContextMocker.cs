using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStore.Core.DataLayer;

namespace OnlineStore.Mocker
{
    public static class DbContextMocker
    {
        private static readonly string ConnectionString;

        static DbContextMocker()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = configuration.GetSection("ConnectionStrings")["OnLineStore"];
        }

        public static OnlineStoreDbContext GetOnlineStoreDbContext()
            => new OnlineStoreDbContext(new DbContextOptionsBuilder<OnlineStoreDbContext>().UseSqlServer(ConnectionString).Options);
    }
}
