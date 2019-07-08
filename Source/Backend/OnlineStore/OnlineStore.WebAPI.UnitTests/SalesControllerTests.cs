using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Helpers;
using OnlineStore.Core.BusinessLayer.Requests;
using OnlineStore.Core.BusinessLayer.Responses;
using OnlineStore.Core.Domain.Sales;
using OnlineStore.WebAPI.Controllers;
using OnlineStore.WebAPI.Requests;
using OnlineStore.WebAPI.UnitTests.Mocks;
using OnlineStore.WebAPI.UnitTests.Mocks.Identity;
using OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway;
using Xunit;

namespace OnlineStore.WebAPI.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestSearchOrdersAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestSearchOrdersAsync));
            var request = new SearchOrdersRequest();

            // Act
            var response = await controller.SearchOrdersAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestSearchOrdersByCurrencyAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestSearchOrdersByCurrencyAsync));
            var request = new SearchOrdersRequest
            {
                CurrencyID = "USD"
            };

            // Act
            var response = await controller.SearchOrdersAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.CurrencyID == request.CurrencyID) == value.Model.Count());
        }

        [Fact]
        public async Task TestSearchOrdersByCustomerAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestSearchOrdersByCustomerAsync));
            var request = new SearchOrdersRequest
            {
                CustomerID = 1
            };

            // Act
            var response = await controller.SearchOrdersAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.CustomerID == request.CustomerID) == value.Model.Count());
        }

        [Fact]
        public async Task TestSearchOrdersByEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSalesController(nameof(TestSearchOrdersByEmployeeAsync));
            var request = new SearchOrdersRequest
            {
                EmployeeID = 1
            };

            // Act
            var response = await controller.SearchOrdersAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count(item => item.EmployeeID == request.EmployeeID) == value.Model.Count());
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
        }

        [Fact]
        public async Task TestPostOrderAsync()
        {
            // Arrange
            //var controller = ControllerMocker.GetSalesController(nameof(TestPostOrderAsync));

            var identityClient = new MockedRothschildHouseIdentityClient();
            var paymentClient = new MockedRothschildHousePaymentClient();

            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestPostOrderAsync), true);
            var controller = new SalesController(LoggingHelper.GetLogger<SalesController>(), identityClient, paymentClient, service);
            var request = new PostOrderRequest
            {
                ID = 2,
                CustomerID = 1,
                PaymentMethodID = new Guid("7671A4F7-A735-4CB7-AAB4-CF47AE20171D"),
                CurrencyID = "USD",
                Comments = "Order from unit tests",
                Details = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest
                    {
                        ID = 2,
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
            Assert.True(value.Model.ID.HasValue);
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
