using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.DataLayer.Sales;
using OnlineStore.Core.EntityLayer.Sales;
using OnlineStore.WebAPI.Requests;
using OnlineStore.WebAPI.UnitTests.Mocks;
using Xunit;

namespace OnlineStore.WebAPI.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestGetOrdersAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetOrdersAsync));

            // Act
            var response = await controller.GetOrdersAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetOrdersByCurrencyAsync));
            var currencyID = "USD";

            // Act
            var response = await controller.GetOrdersAsync(currencyID: currencyID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
            Assert.True(value.Model.Count(item => item.CurrencyID == currencyID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetOrdersByCustomerAsync));
            var customerID = 1;

            // Act
            var response = await controller.GetOrdersAsync(customerID: customerID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.CustomerID == customerID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetOrdersByEmployeeAsync));
            var employeeID = 1;

            // Act
            var response = await controller.GetOrdersAsync(employeeID: employeeID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.EmployeeID == employeeID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetOrderAsync));
            var id = 1;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetNonExistingOrderAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetNonExistingOrderAsync));
            var id = 0;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestGetCreateOrderRequestAsync));

            // Act
            var response = await controller.GetCreateOrderRequestAsync() as ObjectResult;
            var value = response.Value as ISingleResponse<Core.BusinessLayer.Requests.CreateOrderRequest>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Products.Count() > 0);
            Assert.True(value.Model.Customers.Count() > 0);
        }

        [Fact]
        public async Task TestPostOrderAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestPostOrderAsync));
            var request = new PostOrderRequest
            {
                CustomerID = 1,
                PaymentMethodID = new Guid("7671A4F7-A735-4CB7-AAB4-CF47AE20171D"),
                CurrencyID = "USD",
                Comments = "Order from unit tests",
                Details = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest
                    {
                        ProductID = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var response = await controller.PostOrderAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.OrderHeaderID.HasValue);
        }

        [Fact]
        public async Task TestCloneOrderAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestCloneOrderAsync));
            var id = 1;

            // Act
            var response = await controller.CloneOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
