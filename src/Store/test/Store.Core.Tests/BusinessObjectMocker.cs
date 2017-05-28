using Microsoft.Extensions.Options;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;

namespace Store.Core.Tests
{
    public static class BusinessObjectMocker
    {
        public static IHumanResourcesBusinessObject GetHumanResourcesBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<IHumanResourcesBusinessObject>();

            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new HumanResourcesBusinessObject(logger, userInfo, new StoreDbContext(appSettings, entityMapper)) as IHumanResourcesBusinessObject;
        }

        public static IProductionBusinessObject GetProductionBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<IProductionBusinessObject>();

            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new ProductionBusinessObject(logger, userInfo, new StoreDbContext(appSettings, entityMapper)) as IProductionBusinessObject;
        }

        public static ISalesBusinessObject GetSalesBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<ISalesBusinessObject>();

            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new SalesBusinessObject(logger, userInfo, new StoreDbContext(appSettings, entityMapper)) as ISalesBusinessObject;
        }
    }
}
