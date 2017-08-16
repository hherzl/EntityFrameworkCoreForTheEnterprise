using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Controllers;
using Store.Core.BusinessLayer.Responses;
using Store.Core.EntityLayer.Production;
using Xunit;

namespace Store.API.Tests
{
    public class ProductionControllerTests
    {
        [Fact]
        public async Task GetProductsTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<ProductionController>();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();

            using (var controller = new ProductionController(logger, productionBusinessObject))
            {
                // Act
                var response = await controller.GetProductsAsync() as ObjectResult;
                var value = response.Value as IPagingResponse<Product>;

                // Assert
                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetInventoryByProductTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<ProductionController>();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var id = 1;

            using (var controller = new ProductionController(logger, productionBusinessObject))
            {
                // Act
                var response = await controller.GetInventoryByProduct(id) as ObjectResult;
                var value = response.Value as IListResponse<ProductInventory>;

                // Assert
                Assert.False(value.DidError);
            }
        }
    }
}
