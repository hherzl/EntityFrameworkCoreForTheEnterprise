using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnLineStore.Common;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.DataLayer.Sales;
using OnLineStore.Core.EntityLayer.Sales;
using OnLineStore.WebAPI.Controllers;
using OnLineStore.WebAPI.Requests;
using OnLineStore.WebAPI.UnitTests.Mocks;
using Xunit;

namespace OnLineStore.WebAPI.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestGetOrdersAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrdersAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);

            // Act
            var response = await controller.GetOrdersAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrdersByCurrencyAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var currencyID = "USD";

            // Act
            var response = await controller.GetOrdersAsync(currencyID: currencyID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
            Assert.True(value.Model.Count(item => item.CurrencyID == currencyID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrdersByCustomerAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var customerID = 1;

            // Act
            var response = await controller.GetOrdersAsync(customerID: customerID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.CustomerID == customerID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrdersByEmployeeAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var employeeID = 1;

            // Act
            var response = await controller.GetOrdersAsync(employeeID: employeeID) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.EmployeeID == employeeID) == value.Model.Count());
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrderAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var id = 1;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetNonExistingOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetNonExistingOrderAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var id = 0;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetCreateOrderRequestAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);

            // Act
            var response = await controller.GetCreateOrderRequestAsync() as ObjectResult;
            var value = response.Value as ISingleResponse<Core.BusinessLayer.Requests.CreateOrderRequest>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Products.Count() > 0);
            Assert.True(value.Model.Customers.Count() > 0);
        }

        [Fact]
        public async Task TestCreateOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestCreateOrderAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var request = new Requests.CreateOrderRequest
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
            var response = await controller.CreateOrderAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.OrderHeaderID.HasValue);
        }

        [Fact]
        public async Task TestCloneOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestCloneOrderAsync));
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), service);
            var id = 1;

            // Act
            var response = await controller.CloneOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
        }
    }
}
