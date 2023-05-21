using Microsoft.EntityFrameworkCore;
using RothschildHouse.Infrastructure.Core.Persistence;

namespace RothschildHouse.Seed.PaymentGateway.Helpers
{
    internal static class DbContextHelper
    {
        private const string ConnectionString = "server=(local); database=RothschildHouse; integrated security=yes; TrustServerCertificate=True;";

        public static RothschildHouseDbContext GetRothschildHouseDbContext()
            => new(new DbContextOptionsBuilder<RothschildHouseDbContext>().UseSqlServer(ConnectionString).Options, null);
    }
}
