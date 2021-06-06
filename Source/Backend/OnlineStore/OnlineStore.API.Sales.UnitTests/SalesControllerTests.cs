using System;
using System.Collections.Generic;
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
        public async Task GetOrdersAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(GetOrdersAsync), true);
            var controller = new SalesController(null, null, null, service);
            var request = new GetOrdersRequest();

            // Act
            var response = await controller.GetOrdersAsync(request) as ObjectResult;
            var value = response.Value as IPagedResponse<OrderInfo>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task GetOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(GetOrderAsync), true);
            var controller = new SalesController(null, null, null, service);
            var id = 1;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task GetNonExistingOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(GetNonExistingOrderAsync), true);
            var controller = new SalesController(null, null, null, service);
            var id = 0;

            // Act
            var response = await controller.GetOrderAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrderHeader>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task GetCreateOrderRequestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(GetCreateOrderRequestAsync), true);
            var controller = new SalesController(null, null, null, service);

            // Act
            var response = await controller.GetPostOrderModelAsync() as ObjectResult;
            var value = response.Value as ISingleResponse<CreateOrderRequest>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task PostOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(PostOrderAsync), true);
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
        public async Task CloneOrderAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSalesService(userInfo, nameof(CloneOrderAsync), true);
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
