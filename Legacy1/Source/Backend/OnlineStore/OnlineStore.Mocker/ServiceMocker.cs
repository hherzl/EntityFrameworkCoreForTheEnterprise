using OnlineStore.Common.Helpers;
using OnlineStore.Core;
using OnlineStore.Core.Business;
using OnlineStore.Core.Business.Contracts;

namespace OnlineStore.Mocker
{
    public static class ServiceMocker
    {
        public static ISalesService GetSalesService()
            => new SalesService(
                LoggingHelper.GetLogger<SalesService>(),
                DbContextMocker.GetOnlineStoreDbContext(),
                new UserInfo("mocker")
            );

        public static IWarehouseService GetWarehouseService()
            => new WarehouseService(
                LoggingHelper.GetLogger<WarehouseService>(),
                DbContextMocker.GetOnlineStoreDbContext(),
                new UserInfo("mocker")
            );
    }
}
