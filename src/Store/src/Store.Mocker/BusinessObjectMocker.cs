using Microsoft.Extensions.Options;
using Store.Core;
using Store.Core.BusinessLayer;
using Store.Core.BusinessLayer.Contracts;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;

namespace Store.Mocker
{
    public class BusinessObjectMocker
    {
        public static IHumanResourcesBusinessObject GetHumanResourcesBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<IHumanResourcesBusinessObject>();

            var userInfo = new UserInfo { Name = "mocker" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            return new HumanResourcesBusinessObject(logger, userInfo, new StoreDbContext(appSettings, new StoreEntityMapper()));
        }

        public static IProductionBusinessObject GetProductionBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<IProductionBusinessObject>();

            var userInfo = new UserInfo { Name = "mocker" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            return new ProductionBusinessObject(logger, userInfo, new StoreDbContext(appSettings, new StoreEntityMapper()));
        }

        public static ISalesBusinessObject GetSalesBusinessObject()
        {
            var logger = LoggerMocker.GetLogger<ISalesBusinessObject>();

            var userInfo = new UserInfo { Name = "mocker" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            return new SalesBusinessObject(logger, userInfo, new StoreDbContext(appSettings, new StoreEntityMapper()));
        }
    }
}
