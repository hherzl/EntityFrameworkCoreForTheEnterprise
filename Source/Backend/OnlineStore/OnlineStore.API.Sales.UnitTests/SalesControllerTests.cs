using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Common.UnitTests.Mocks;
using OnlineStore.API.Sales.Controllers;
using OnlineStore.API.Sales.Requests;
using OnlineStore.API.Sales.UnitTests.Mocks;
using OnlineStore.Core.Business.Requests;
using OnlineStore.Core.Business.Responses;
using OnlineStore.Core.Domain.Sales;
using Xunit;

namespace OnlineStore.API.Sales.UnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public async Task TestSearchOrdersAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestSearchOrdersAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestSearchOrdersByCurrencyAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestSearchOrdersByCustomerAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestSearchOrdersByEmployeeAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetOrderAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetNonExistingOrderAsync), true);
            var controller = new SalesController(null, null, null, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestGetCreateOrderRequestAsync), true);
            var controller = new SalesController(null, null, null, service);

            // Act
            var response = await controller.GetCreateOrderRequestAsync() as ObjectResult;
            var value = response.Value as ISingleResponse<CreateOrderRequest>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestPostOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestPostOrderAsync), true);
            var identityClient = new MockedRothschildHouseIdentityClient();
            var paymentClient = new MockedRothschildHousePaymentClient();
            var controller = new SalesController(null, identityClient, paymentClient, service);
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
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(TestCloneOrderAsync), true);
            var controller = new SalesController(null, null, null, service);
            var id = 1;

            // Act
            var response = await controller.CloneOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
