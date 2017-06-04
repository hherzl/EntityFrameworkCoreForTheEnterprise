using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Controllers;
using Store.API.ViewModels;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.API.Tests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task GetOrdersTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<SalesController>();
            var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();

            using (var controller = new SalesController(logger, humanResourcesBusinessObject, productionBusinessObject, salesBusinessObject))
            {
                // Act
                var response = await controller.GetOrders() as ObjectResult;

                // Assert
                var value = response.Value as IPagingModelResponse<OrderInfo>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetOrderTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<SalesController>();
            var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();
            var id = 1;

            using (var controller = new SalesController(logger, humanResourcesBusinessObject, productionBusinessObject, salesBusinessObject))
            {
                // Act
                var response = await controller.GetOrder(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<Order>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetNonExistingOrderTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<SalesController>();
            var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();
            var id = 0;

            using (var controller = new SalesController(logger, humanResourcesBusinessObject, productionBusinessObject, salesBusinessObject))
            {
                // Act
                var response = await controller.GetOrder(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<Order>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetCreateOrderViewModelTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<SalesController>();
            var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();

            using (var controller = new SalesController(logger, humanResourcesBusinessObject, productionBusinessObject, salesBusinessObject))
            {
                // Act
                var response = await controller.GetCreateOrderViewModel() as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<CreateOrderViewModel>;

                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task GetCloneOrderTestAsync()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<SalesController>();
            var humanResourcesBusinessObject = BusinessObjectMocker.GetHumanResourcesBusinessObject();
            var productionBusinessObject = BusinessObjectMocker.GetProductionBusinessObject();
            var salesBusinessObject = BusinessObjectMocker.GetSalesBusinessObject();
            var id = 1;

            using (var controller = new SalesController(logger, humanResourcesBusinessObject, productionBusinessObject, salesBusinessObject))
            {
                // Act
                var response = await controller.CloneOrder(id) as ObjectResult;

                // Assert
                var value = response.Value as ISingleModelResponse<Order>;

                Assert.False(value.DidError);
            }
        }
    }
}
