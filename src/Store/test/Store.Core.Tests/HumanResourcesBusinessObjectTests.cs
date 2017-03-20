using System;
using System.Threading.Tasks;
using Store.Core.EntityLayer.HumanResources;
using Xunit;

namespace Store.Core.Tests
{
    public class HumanResourcesBusinessObjectTests
    {
        [Fact]
        public async Task TestGetEmployees()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await businessObject.GetEmployeesAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestUpdateEmployee()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject())
            {
                var changes = new Employee
                {
                    EmployeeID = 1,
                    FirstName = "John III",
                    MiddleName = "Smith III",
                    LastName = "Doe III",
                    BirthDate = DateTime.Now.AddYears(-18)
                };

                // Act
                var response = await businessObject.UpdateEmployeeAsync(changes);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
