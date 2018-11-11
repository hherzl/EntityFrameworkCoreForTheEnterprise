using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.Mocker
{
    public static class DbContextMocker
    {
        private static readonly string ConnectionString;

        static DbContextMocker()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            ConnectionString = configuration.GetSection("ConnectionStrings")["OnLineStore"];
        }

        public static OnLineStoreDbContext GetOnLineStoreDbContext()
            => new OnLineStoreDbContext(new DbContextOptionsBuilder<OnLineStoreDbContext>().UseSqlServer(ConnectionString).Options);
    }
}
