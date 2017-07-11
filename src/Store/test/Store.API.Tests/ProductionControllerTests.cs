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
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();

            using (var controller = new ProductionController(null, productionBusinessObject))
            {
                // Act
                var response = await controller.GetProductsAsync() as ObjectResult;

                // Assert
                var value = response.Value as IListModelResponse<Product>;

                Assert.False(value.DidError);
            }
        }
    }
}
