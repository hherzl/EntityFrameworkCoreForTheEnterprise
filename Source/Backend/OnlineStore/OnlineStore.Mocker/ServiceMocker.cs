using OnlineStore.Common;
using OnlineStore.Core;
using OnlineStore.Core.BusinessLayer;
using OnlineStore.Core.BusinessLayer.Contracts;

namespace OnlineStore.Mocker
{
    public static class ServiceMocker
    {
        public static ISalesService GetSalesService()
            => new SalesService(
                LoggingHelper.GetLogger<SalesService>(),
                new UserInfo("mocker"),
                DbContextMocker.GetOnlineStoreDbContext()
                );

        public static IWarehouseService GetWarehouseService()
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                new UserInfo("mocker"),
                DbContextMocker.GetOnlineStoreDbContext()
            );
    }
}
