using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace OnLineStore.WebAPI.IntegrationTests
{
    public class WarehouseTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public WarehouseTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetProductsTestAsync()
        {
            // Arrange
            var request = "/api/v1/Warehouse/Product";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductInventoriesAsync()
        {
            // Arrange
            var request = string.Format("/api/v1/Warehouse/ProductInventory");

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
