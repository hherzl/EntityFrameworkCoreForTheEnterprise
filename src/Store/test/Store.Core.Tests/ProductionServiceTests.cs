using System.Threading.Tasks;
using Xunit;

namespace Store.Core.Tests
{
    public class ProductionServiceTests
    {
        [Fact]
        public async Task TestGetProducts()
        {
            // Arrange
            using (var service = ServiceMocker.GetProductionService())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await service.GetProductsAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
