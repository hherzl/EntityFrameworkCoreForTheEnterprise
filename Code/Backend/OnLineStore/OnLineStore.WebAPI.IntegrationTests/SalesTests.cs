using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnLineStore.WebAPI.IntegrationTests.Helpers;
using Xunit;

namespace OnLineStore.WebAPI.IntegrationTests
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
            var request = "/api/v1/Sales/Order";
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsCustomerAsync()
        {
            // Arrange
            var currencyID = (short)1;
            var request = string.Format("/api/v1/Sales/Order?currencyID={0}", currencyID);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsCustomerAsync()
        {
            // Arrange
            var customerID = 1;
            var request = string.Format("/api/v1/Sales/Order?customerID={0}", customerID);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsCustomerAsync()
        {
            // Arrange
            var employeeID = 1;
            var request = string.Format("/api/v1/Sales/Order?employeeID={0}", employeeID);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByIdAsCustomerAsync()
        {
            // Arrange
            var id = 1;
            var request = string.Format("/api/v1/Sales/Order/{0}", id);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByNonExistingIdAsCustomerAsync()
        {
            // Arrange
            var id = 0;
            var request = string.Format("/api/v1/Sales/Order/{0}", id);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsCustomerAsync()
        {
            // Arrange
            var request = "/api/v1/Sales/CreateOrderRequest";
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCreateOrderAsCustomerAsync()
        {
            // Arrange
            var request = "/api/v1/Sales/Order";
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();
            var model = new
            {
                UserName = "charlesxavier@gmail.com",
                Password = "professorx",
                CardHolderName = "Charles F Xavier",
                IssuingNetwork = "Visa",
                CardNumber = "4024007164051145",
                ExpirationDate = new DateTime(DateTime.Now.Year + 5, DateTime.Now.Month, 1),
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
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.PostAsync(request, ContentHelper.GetStringContent(model));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCloneOrderAsCustomerAsync()
        {
            // Arrange
            var id = 1;
            var request = string.Format("/api/v1/Sales/CloneOrder/{0}", id);
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
