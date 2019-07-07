using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.WebAPI.IntegrationTests.Helpers;
using Xunit;

namespace OnlineStore.WebAPI.IntegrationTests
{
    public class SalesTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient apiClient;

        public SalesTests(TestFixture<Startup> fixture)
        {
            apiClient = fixture.Client;
        }

        [Fact]
        public async Task TestSearchOrdersAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByCurrencyAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    CurrencyID = 1
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByCustomerAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    CustomerID = 1
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByEmployeeAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    EmployeeID = 1
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 1)
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByNonExistingIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 0)
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/CreateOrderRequest"
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsWarehouseOperatorAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseOperatorAsync();
            var request = new
            {
                Url = "/api/v1/Sales/CreateOrderRequest"
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task TestPostOrderAsCustomerAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Sales/Order",
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
            var token = await TokenHelper.GetOnlineStoreCustomerTokenAsync(request.Body.UserName, request.Body.Password);

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCloneOrderAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/CloneOrder/{0}", 1)
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
