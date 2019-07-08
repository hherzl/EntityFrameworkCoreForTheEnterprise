using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.WebAPI.IntegrationTests.Helpers;
using Xunit;

namespace OnlineStore.WebAPI.IntegrationTests
{
    public class WarehouseTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient apiClient;

        public WarehouseTests(TestFixture<Startup> fixture)
        {
            apiClient = fixture.Client;
        }

        [Fact]
        public async Task GetProductsAsCustomerTestAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/Product"
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoriesAsWarehouseOperatorAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseOperatorAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/ProductInventory/1")
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoriesAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/ProductInventory/1")
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PostProductAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/Product"),
                Body = new
                {
                    ProductName = string.Format("Test product (integration tests) {0}", DateTime.Now),
                    ProductCategoryID = 1,
                    UnitPrice = 9.99m,
                    Description = "unit tests"
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PutProductUnitPriceAsWarehouseManagerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetOnlineStoreTokenForWarehouseManagerAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/ProductUnitPrice/{0}", 1),
                Body = new
                {
                    UnitPrice = 14.99m
                }
            };

            // Act
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
