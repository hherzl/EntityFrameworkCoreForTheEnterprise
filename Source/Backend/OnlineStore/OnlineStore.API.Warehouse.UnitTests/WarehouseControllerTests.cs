using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Common.UnitTests.Mocks;
using OnlineStore.API.Warehouse.Controllers;
using OnlineStore.API.Warehouse.Requests;
using OnlineStore.API.Warehouse.UnitTests.Mocks;
using OnlineStore.Core.Business.Requests;
using OnlineStore.Core.Business.Responses;
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
            var controller = new WarehouseController(null, service);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestGetInventoryByProductTestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestGetInventoryByProductTestAsync), true);
            var controller = new WarehouseController(null, service);
            var productID = 1;
            var locationID = "W01";

            // Act
            var response = await controller.GetProductInventoryAsync(productID, locationID) as ObjectResult;
            var value = response.Value as IListResponse<ProductInventory>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestCreateProductTestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestCreateProductTestAsync));
            var controller = new WarehouseController(null, service);
            var request = new PostProductRequest
            {

                ProductName = "Test product",
                ProductCategoryID = 100,
                UnitPrice = 9.99m,
                Description = "unit tests"
            };

            // Act
            var response = await controller.PostProductAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestUpdateProductUnitPriceAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestUpdateProductUnitPriceAsync));
            var controller = new WarehouseController(null, service);
            var id = 1;
            var request = new UpdateProductUnitPriceRequest
            {
                UnitPrice = 14.99m
            };

            // Act
            var response = await controller.PutProductUnitPriceAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<Product>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
