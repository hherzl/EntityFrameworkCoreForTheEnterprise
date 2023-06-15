using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RothschildHouse.Infrastructure.Core.Persistence;

namespace RothschildHouse.Seed.PaymentGateway.Helpers
{
    internal static class DbContextHelper
    {
        private static readonly IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            ;

        public static RothschildHouseDbContext GetRothschildHouseDbContext()
            => new(new DbContextOptionsBuilder<RothschildHouseDbContext>().UseSqlServer(_config.GetConnectionString("RothschildHouse")).Options, null);
    }
}
