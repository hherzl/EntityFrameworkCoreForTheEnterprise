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
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Warehouse/Product"
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoriesAsCustomerAsync()
        {
            // Arrange
            var customerToken = await TokenHelper.GetOnlineStoreCustomerTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Warehouse/ProductInventory/1")
            };

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
