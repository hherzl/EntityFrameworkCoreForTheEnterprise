using Xunit;

namespace Store.Core.Tests
{
    public class ProductionBusinessObjectTests
    {
        [Fact]
        public void TestGetProducts()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetProductionBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetProducts(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
