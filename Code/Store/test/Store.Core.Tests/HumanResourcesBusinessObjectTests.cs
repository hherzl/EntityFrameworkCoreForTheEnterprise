using Xunit;

namespace Store.Core.Tests
{
    public class HumanResourcesBusinessObjectTests
    {
        [Fact]
        public void TestGetEmployees()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = businessObject.GetEmployees(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
