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
        public async Task GetProductsTestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<ProductionController>();
            var productionService = ServiceMocker.GetProductionService();

            using (var controller = new ProductionController(logger, productionService))
            {
                // Act
                var response = await controller.GetProductsAsync() as ObjectResult;
                var value = response.Value as IPagedResponse<Product>;

                // Assert
                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetInventoryByProductTestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<ProductionController>();
            var productionService = ServiceMocker.GetProductionService();
            var id = 1;

            using (var controller = new ProductionController(logger, productionService))
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
