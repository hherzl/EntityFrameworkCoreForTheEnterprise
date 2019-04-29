using OnlineStore.Common.Helpers;
using OnlineStore.Core;
using OnlineStore.Core.BusinessLayer;
using OnlineStore.Core.BusinessLayer.Contracts;

namespace OnlineStore.WebAPI.UnitTests.Mocks
{
    public static class ServiceMocker
    {
        public static IHumanResourcesService GetHumanResourcesService(IUserInfo userInfo, string dbName)
            => new HumanResourcesService(
                LoggingHelper.GetLogger<HumanResourcesService>(),
                userInfo,
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName)
            );

        public static ISalesService GetSalesService(IUserInfo userInfo, string dbName, bool seedWarehouseSchema = false)
            => new SalesService(
                LoggingHelper.GetLogger<SalesService>(),
                userInfo,
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName, seedWarehouseSchema)
            );

        public static IWarehouseService GetWarehouseService(IUserInfo userInfo, string dbName, bool seedWarehouseSchema = false)
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                userInfo,
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName, seedWarehouseSchema)
                );
    }
}
