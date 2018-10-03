using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Controllers;
using Store.Common;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Production;
using Xunit;

namespace Store.API.UnitTests
{
    public class ProductionControllerTests
    {
        [Fact]
        public async Task TestGetProductsTestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<ProductionController>();
            var productionService = ServiceMocker.GetProductionService(nameof(TestGetProductsTestAsync));
            var controller = new ProductionController(logger, productionService);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestGetInventoryByProductTestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<ProductionController>();
            var productionService = ServiceMocker.GetProductionService(nameof(TestGetInventoryByProductTestAsync));
            var controller = new ProductionController(logger, productionService);
            var productID = 1;
            var warehouseID = "W0001";

            // Act
            var response = await controller.GetProductInventoryAsync(productID, warehouseID) as ObjectResult;
            var value = response.Value as IListResponse<ProductInventory>;

            controller.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }
    }
}
