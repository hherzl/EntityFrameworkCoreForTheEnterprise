using Store.Common;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;

namespace Store.API.UnitTests
{
    public static class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService()
            => new HumanResourcesService(
                LogHelper.GetLogger<HumanResourcesService>(),
                new UserInfo { Name = "unittests" },
                new StoreDbContext(DbContextOptionsMocker.GetDbContextOptions("StoreDbInMemory"))
                );

        public static IProductionService GetProductionService()
            => new ProductionService(
                LogHelper.GetLogger<ProductionService>(),
                new UserInfo { Name = "unittests" },
                new StoreDbContext(DbContextOptionsMocker.GetDbContextOptions("StoreDbInMemory"))
                );

        public static ISalesService GetSalesService()
            => new SalesService(
                LogHelper.GetLogger<SalesService>(),
                new UserInfo { Name = "unittests" },
                new StoreDbContext(DbContextOptionsMocker.GetDbContextOptions("StoreDbInMemory"))
                );
    }
}
