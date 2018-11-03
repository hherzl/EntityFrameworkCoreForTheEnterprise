using Store.Common;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;

namespace Store.Mocker
{
    public static class ServiceMocker
    {
        public static IProductionService GetProductionService()
            => new ProductionService(LogHelper.GetLogger<ProductionService>(), new UserInfo("mocker"), DbContextMocker.GetStoreDbContext());

        public static ISalesService GetSalesService()
            => new SalesService(LogHelper.GetLogger<SalesService>(), new UserInfo("mocker"), DbContextMocker.GetStoreDbContext());
    }
}
