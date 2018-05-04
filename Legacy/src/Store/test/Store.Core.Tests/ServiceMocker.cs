using Microsoft.EntityFrameworkCore;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;

namespace Store.Core.Tests
{
    public static class ServiceMocker
    {
        private static string ConnectionString
            => "server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;";

        public static IHumanResourcesService GetHumanResourcesService()
        {
            var logger = LoggerMocker.GetLogger<IHumanResourcesService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new HumanResourcesService(logger, new UserInfo { Name = "admin" }, new StoreDbContext(options));
        }

        public static IProductionService GetProductionService()
        {
            var logger = LoggerMocker.GetLogger<IProductionService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new ProductionService(logger, new UserInfo { Name = "admin" }, new StoreDbContext(options));
        }

        public static ISalesService GetSalesService()
        {
            var logger = LoggerMocker.GetLogger<ISalesService>();

            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new SalesService(logger, new UserInfo { Name = "admin" }, new StoreDbContext(options));
        }
    }
}
