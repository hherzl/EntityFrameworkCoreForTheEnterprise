using OnLineStore.Common;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;

namespace OnLineStore.WebAPI.UnitTests
{
    public static class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService(string dbName)
            => new HumanResourcesService(LoggingHelper.GetLogger<HumanResourcesService>(), new UserInfo("unittests"), DbContextMocker.GetOnLineStoreDbContextInMemory(dbName));

        public static IProductionService GetProductionService(string dbName)
            => new ProductionService(LoggingHelper.GetLogger<ProductionService>(), new UserInfo("unittests"), DbContextMocker.GetOnLineStoreDbContextInMemory(dbName));

        public static ISalesService GetSalesService(string dbName)
            => new SalesService(LoggingHelper.GetLogger<SalesService>(), new UserInfo("unittests"), DbContextMocker.GetOnLineStoreDbContextInMemory(dbName));
    }
}
