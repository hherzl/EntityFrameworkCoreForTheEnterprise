using System;
using Store.Core.EntityLayer.HumanResources;
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

        [Fact]
        public void TestUpdateEmployee()
        {
            // Arrange
            using (var businessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject())
            {
                var changes = new Employee
                {
                    EmployeeID = 1,
                    FirstName = "John",
                    MiddleName = "Smith",
                    LastName = "Doe",
                    BirthDate = new DateTime(2017, 1, 6)
                };

                // Act
                var response = businessObject.UpdateEmployee(changes);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
