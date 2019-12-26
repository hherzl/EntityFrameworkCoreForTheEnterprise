using OnlineStore.Common.Helpers;
using OnlineStore.Core;
using OnlineStore.Core.BusinessLayer;
using OnlineStore.Core.BusinessLayer.Contracts;

namespace OnlineStore.API.Common.UnitTests.Mocks
{
    public static class ServiceMocker
    {
        public static IWarehouseService GetWarehouseService(IUserInfo userInfo, string dbName, bool seedWarehouseSchema = false)
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName, seedWarehouseSchema),
                userInfo
            );

        public static ISalesService GetSalesService(IUserInfo userInfo, string dbName, bool seedSalesSchema = false)
            => new SalesService(
                LoggingHelper.GetLogger<SalesService>(),
                DbContextMocker.GetOnlineStoreDbContextInMemory(dbName, seedSalesSchema),
                userInfo
            );
    }
}
