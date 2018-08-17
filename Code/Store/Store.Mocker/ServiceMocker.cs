using Microsoft.EntityFrameworkCore;
using Store.Common;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;

namespace Store.Mocker
{
    public static class ServiceMocker
    {
        private static readonly string ConnectionString;

        static ServiceMocker()
        {
            // todo: Load connection string from appsettings.json file
            ConnectionString = "server=(local);database=Store;integrated security=yes;MultipleActiveResultSets=True;";
        }

        public static IHumanResourcesService GetHumanResourcesService()
            => new HumanResourcesService(
                LogHelper.GetLogger<HumanResourcesService>(),
                new UserInfo { Name = "mocker" },
                new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseSqlServer(ConnectionString).Options)
            );

        public static IProductionService GetProductionService()
            => new ProductionService(
                LogHelper.GetLogger<ProductionService>(),
                new UserInfo { Name = "mocker" },
                new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseSqlServer(ConnectionString).Options)
            );

        public static ISalesService GetSalesService()
            => new SalesService(
                LogHelper.GetLogger<SalesService>(),
                new UserInfo { Name = "mocker" },
                new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseSqlServer(ConnectionString).Options)
            );
    }
}
