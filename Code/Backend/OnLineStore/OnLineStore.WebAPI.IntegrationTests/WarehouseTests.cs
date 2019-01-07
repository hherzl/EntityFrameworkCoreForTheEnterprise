using System.Net.Http;
using System.Threading.Tasks;
using OnLineStore.WebAPI.IntegrationTests.Helpers;
using Xunit;

namespace OnLineStore.WebAPI.IntegrationTests
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
            var request = "/api/v1/Warehouse/Product";
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoriesAsCustomerAsync()
        {
            // Arrange
            var request = string.Format("/api/v1/Warehouse/ProductInventory/1");
            var customerToken = await IdentityServerHelper.GetValidCustomerTokenAsync();

            // Act
            apiClient.SetBearerToken(customerToken.AccessToken);

            var response = await apiClient.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
