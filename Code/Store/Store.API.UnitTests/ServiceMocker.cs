using Microsoft.EntityFrameworkCore;
using Store.Common;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;

namespace Store.API.UnitTests
{
    public static class ServiceMocker
    {
        private static string ConnectionString
            => "server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;";

        public static IHumanResourcesService GetHumanResourcesService()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new HumanResourcesService(LogHelper.GetLogger<HumanResourcesService>(), new UserInfo { Name = "qa" }, new StoreDbContext(options));
        }

        public static IProductionService GetProductionService()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new ProductionService(LogHelper.GetLogger<ProductionService>(), new UserInfo { Name = "qa" }, new StoreDbContext(options));
        }

        public static ISalesService GetSalesService()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new SalesService(LogHelper.GetLogger<SalesService>(), new UserInfo { Name = "qa" }, new StoreDbContext(options));
        }
    }
}
