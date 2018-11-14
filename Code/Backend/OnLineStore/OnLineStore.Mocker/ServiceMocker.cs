using OnLineStore.Common;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;

namespace OnLineStore.Mocker
{
    public static class ServiceMocker
    {
        public static IProductionService GetProductionService()
            => new ProductionService(LoggingHelper.GetLogger<ProductionService>(), new UserInfo("mocker"), DbContextMocker.GetOnLineStoreDbContext());

        public static ISalesService GetSalesService()
            => new SalesService(LoggingHelper.GetLogger<SalesService>(), new UserInfo("mocker"), DbContextMocker.GetOnLineStoreDbContext());
    }
}
