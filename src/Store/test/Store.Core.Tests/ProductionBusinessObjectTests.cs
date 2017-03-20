using System.Threading.Tasks;
using Xunit;

namespace Store.Core.Tests
{
    public class ProductionBusinessObjectTests
    {
        [Fact]
        public async Task TestGetProducts()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetProductionBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await businessObject.GetProductsAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
