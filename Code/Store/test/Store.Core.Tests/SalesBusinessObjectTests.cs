using Xunit;

namespace Store.Core.Tests
{
    public class SalesBusinessObjectTests
    {
        [Fact]
        public void TestGetCustomers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetCustomers(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetEmployees()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetEmployees(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetShippers()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetShippers(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetProducts()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetProducts(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public void TestGetOrders()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetSalesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetOrders(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
