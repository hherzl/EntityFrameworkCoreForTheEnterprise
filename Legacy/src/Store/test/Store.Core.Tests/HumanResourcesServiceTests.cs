using System;
using System.Threading.Tasks;
using Store.Core.EntityLayer.HumanResources;
using Xunit;

namespace Store.Core.Tests
{
    public class HumanResourcesServiceTests
    {
        [Fact]
        public async Task TestGetEmployees()
        {
            // Arrange
            using (var service = ServiceMocker.GetHumanResourcesService())
            {
                var pageSize = 10;
                var pageNumber = 1;

                // Act
                var response = await service.GetEmployeesAsync(pageSize, pageNumber);

                // Assert
                Assert.False(response.DidError);
            }
        }

        [Fact]
        public async Task TestUpdateEmployee()
        {
            // Arrange
            using (var service = ServiceMocker.GetHumanResourcesService())
            {
                var entity = (await service.GetEmployeeAsync(new Employee { EmployeeID = 1 })).Model;

                entity.FirstName = "John III";
                entity.MiddleName = "Smith III";
                entity.LastName = "Doe III";
                entity.BirthDate = DateTime.Now.AddYears(-18);

                // Act
                var response = await service.UpdateEmployeeAsync(entity);

                // Assert
                Assert.False(response.DidError);
            }
        }
    }
}
