using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Controllers;
using Store.Common;
using Store.Core.BusinessLayer.Requests;
using Store.Core.BusinessLayer.Responses;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.Sales;
using Xunit;

namespace Store.API.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestGetOrdersAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrdersAsync() as ObjectResult;
                var value = response.Value as IPagedResponse<OrderInfo>;

                // Assert
                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var currencyID = (short?)1;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrdersAsync(currencyID: currencyID) as ObjectResult;
                var value = response.Value as IPagedResponse<OrderInfo>;

                // Assert
                Assert.False(value.DidError);
                Assert.True(value.Model.Where(item => item.CurrencyID == currencyID).Count() == value.Model.Count());
            }
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var customerID = 1;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrdersAsync(customerID: customerID) as ObjectResult;
                var value = response.Value as IPagedResponse<OrderInfo>;

                // Assert
                Assert.False(value.DidError);
                Assert.True(value.Model.Where(item => item.CustomerID == customerID).Count() == value.Model.Count());
            }
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var employeeID = 1;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrdersAsync(employeeID: employeeID) as ObjectResult;
                var value = response.Value as IPagedResponse<OrderInfo>;

                // Assert
                Assert.False(value.DidError);
                Assert.True(value.Model.Where(item => item.EmployeeID == employeeID).Count() == value.Model.Count());
            }
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var id = 1;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrderAsync(id) as ObjectResult;
                var value = response.Value as ISingleResponse<Order>;

                // Assert
                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task TestGetNonExistingOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var id = 0;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetOrderAsync(id) as ObjectResult;
                var value = response.Value as ISingleResponse<Order>;

                // Assert
                Assert.False(value.DidError);
            }
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.GetCreateOrderRequestAsync() as ObjectResult;
                var value = response.Value as ISingleResponse<CreateOrderRequest>;

                // Assert
                Assert.False(value.DidError);
                Assert.True(value.Model.Products.Count() >= 0);
            }
        }

        [Fact]
        public async Task TestCloneOrderAsync()
        {
            // Arrange
            var logger = LogHelper.GetLogger<SalesController>();
            var salesService = ServiceMocker.GetSalesService();
            var id = 1;

            using (var controller = new SalesController(logger, salesService))
            {
                // Act
                var response = await controller.CloneOrderAsync(id) as ObjectResult;
                var value = response.Value as ISingleResponse<Order>;

                // Assert
                Assert.False(value.DidError);
            }
        }
    }
}
