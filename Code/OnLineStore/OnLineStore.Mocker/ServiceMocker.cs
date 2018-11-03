using OnLineStore.Common;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;

namespace OnLineStore.Mocker
{
    public static class ServiceMocker
    {
        public static IProductionService GetProductionService()
            => new ProductionService(LogHelper.GetLogger<ProductionService>(), new UserInfo("mocker"), DbContextMocker.GetStoreDbContext());

        public static ISalesService GetSalesService()
            => new SalesService(LogHelper.GetLogger<SalesService>(), new UserInfo("mocker"), DbContextMocker.GetStoreDbContext());
    }
}
