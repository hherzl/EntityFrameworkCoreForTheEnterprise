using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Common.UnitTests.Mockers;
using OnlineStore.API.Warehouse.Controllers;
using OnlineStore.API.Warehouse.UnitTests.Mockers;
using OnlineStore.Common.Helpers;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.Domain.Warehouse;
using Xunit;

namespace OnlineStore.API.Warehouse.UnitTests
{
    public class WarehouseControllerTests
    {
        [Fact]
        public async Task TestGetProductsAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestGetProductsAsync), true);
            var controller = new WarehouseController(LoggingHelper.GetLogger<WarehouseController>(), service);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }
    }
}
