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
        public async Task SearchProductsAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(SearchProductsAsync), true);
            var controller = new WarehouseController(null, service);
            var request = new SearchProductsRequest();

            // Act
            var response = await controller.SearchProductsAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task GetInventoryByProductAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetWarehouseOperatorIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(GetInventoryByProductAsync), true);
            var controller = new WarehouseController(null, service);
            var request = new GetProductInventoryRequest
            {
                ProductID = 1,
                LocationID = "W01"
            };

            // Act
            var response = await controller.GetProductInventoryAsync(request) as ObjectResult;
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
            var response = await controller.UpdateProductUnitPriceAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<Product>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
