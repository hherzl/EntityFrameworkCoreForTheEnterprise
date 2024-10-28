using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.API.Common.IntegrationTests;
using OnlineStore.API.Common.IntegrationTests.Helpers;
using Xunit;

namespace OnlineStore.API.Sales.IntegrationTests
{
    public class SalesTests : IClassFixture<TestFixture<Startup>>
    {
        readonly HttpClient Client;

        public SalesTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetOrdersAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order?pageSize=10&pageNumber=1"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetOrderByIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order/1"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetOrderByNonExistingIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order/0"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPostOrderRequestAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order-model"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetPlaceOrderRequestAsWarehouseOperatorAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWarehouseOperatorAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order-model"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PlaceOrderAsCustomerAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Sales/order",
                Body = new
                {
                    UserName = "jameslogan@walla.com",
                    Password = "wolverine",
                    CardHolderName = "James Logan",
                    IssuingNetwork = "Visa",
                    CardNumber = "4024007164051145",
                    ExpirationDate = new DateTime(2024, 6, 1),
                    Cvv = "987",
                    Total = 29.99m,
                    CustomerID = 1,
                    CurrencyID = "USD",
                    PaymentMethodID = new Guid("7671A4F7-A735-4CB7-AAB4-CF47AE20171D"),
                    Comments = "Order from integration tests",
                    Details = new[]
                    {
                        new
                        {
                            ProductID = 1,
                            Quantity = 1
                        }
                    }
                }
            };

            var token = await TokenHelper
                .GetOnlineStoreTokenAsync(request.Body.UserName, request.Body.Password);

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CloneOrderAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/order/1/clone"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
