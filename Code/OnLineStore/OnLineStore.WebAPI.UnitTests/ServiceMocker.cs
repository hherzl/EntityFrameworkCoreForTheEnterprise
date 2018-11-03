using OnLineStore.Common;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;

namespace OnLineStore.WebAPI.UnitTests
{
    public static class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService(string dbName)
            => new HumanResourcesService(LogHelper.GetLogger<HumanResourcesService>(), new UserInfo("unittests"), DbContextMocker.GetStoreDbContextInMemory(dbName));

        public static IProductionService GetProductionService(string dbName)
            => new ProductionService(LogHelper.GetLogger<ProductionService>(), new UserInfo("unittests"), DbContextMocker.GetStoreDbContextInMemory(dbName));

        public static ISalesService GetSalesService(string dbName)
            => new SalesService(LogHelper.GetLogger<SalesService>(), new UserInfo("unittests"), DbContextMocker.GetStoreDbContextInMemory(dbName));
    }
}
