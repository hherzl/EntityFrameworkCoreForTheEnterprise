using OnLineStore.Common;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;

namespace OnLineStore.WebAPI.UnitTests.Mocks
{
    public static class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService(IUserInfo userInfo, string dbName)
            => new HumanResourcesService(
                LoggingHelper.GetLogger<HumanResourcesService>(),
                userInfo,
                DbContextMocker.GetOnLineStoreDbContextInMemory(dbName)
            );

        public static ISalesService GetSalesService(IUserInfo userInfo, string dbName)
            => new SalesService(
                LoggingHelper.GetLogger<SalesService>(),
                userInfo,
                DbContextMocker.GetOnLineStoreDbContextInMemory(dbName)
            );

        public static IWarehouseService GetWarehouseService(IUserInfo userInfo, string dbName)
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                userInfo,
                DbContextMocker.GetOnLineStoreDbContextInMemory(dbName)
                );
    }
}
