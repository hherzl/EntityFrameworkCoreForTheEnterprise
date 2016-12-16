using Microsoft.Extensions.Options;
using Store.Core.BusinessLayer;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;

namespace Store.Core.Tests
{
    public static class BusinessObjectMocker
    {
        public static ISalesBusinessObject GetSalesBusinessObject()
        {
            var userInfo = new UserInfo { Name = "admin" };

            var appSettings = Options.Create(AppSettingsMocker.Default);

            var entityMapper = new StoreEntityMapper() as IEntityMapper;

            return new SalesBusinessObject(userInfo, new StoreDbContext(appSettings, entityMapper)) as ISalesBusinessObject;
        }
    }
}
