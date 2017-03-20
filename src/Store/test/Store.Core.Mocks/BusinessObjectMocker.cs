using Microsoft.Extensions.Options;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;

namespace Store.Core.Mocks
{
    public static class BusinessObjectMocker
    {
        public static IHumanResourcesBusinessObject GetHumanResourcesBusinessObject()
        {
            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new HumanResourcesBusinessObject(userInfo, new StoreDbContext(appSettings, entityMapper)) as IHumanResourcesBusinessObject;
        }

        public static IProductionBusinessObject GetProductionBusinessObject()
        {
            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new ProductionBusinessObject(userInfo, new StoreDbContext(appSettings, entityMapper)) as IProductionBusinessObject;
        }

        public static ISalesBusinessObject GetSalesBusinessObject()
        {
            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new SalesBusinessObject(userInfo, new StoreDbContext(appSettings, entityMapper)) as ISalesBusinessObject;
        }
    }
}
