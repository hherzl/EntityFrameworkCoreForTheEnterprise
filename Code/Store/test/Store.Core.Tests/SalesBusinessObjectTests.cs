using Microsoft.Extensions.Options;
using Store.Core.BusinessLayer;
using Store.Core.DataLayer;
using Store.Core.DataLayer.Mapping;
using Xunit;

namespace Store.Core.Tests
{
    public class SalesBusinessObjectTests
    {
        private ISalesBusinessObject SalesBusinessObject
        {
            get
            {
                var appSettings = Options.Create(AppSettingsMock.Default);

                var entityMapper = new StoreEntityMapper() as IEntityMapper;

                return new SalesBusinessObject(new StoreDbContext(appSettings, entityMapper)) as ISalesBusinessObject;
            }
        }

        [Fact]
        public void TestGetOrders()
        {
            // Arrange
            using (var businessObject = SalesBusinessObject)
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetOrders(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
