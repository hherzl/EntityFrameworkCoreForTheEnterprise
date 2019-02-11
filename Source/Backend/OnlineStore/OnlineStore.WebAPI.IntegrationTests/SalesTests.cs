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
        public async Task TestGetOrdersAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/Order"
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order?currencyID={0}", 1)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order?customerID={0}", 1)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order?employeeID={0}", 1)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByIdAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 1)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByNonExistingIdAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 0)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/CreateOrderRequest"
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
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

            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenAsync(request.Body.UserName, request.Body.Password);

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCloneOrderAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/CloneOrder/{0}", 1)
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
