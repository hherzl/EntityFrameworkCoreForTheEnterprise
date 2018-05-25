using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests
{
    public class ProductionTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public ProductionTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetProductsTestAsync()
        {
            // Arrange
            var request = "/api/v1/Production/Product";

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetInventoryByProductTestAsync()
        {
            // Arrange
            var id = 1;
            var request = string.Format("/api/v1/Production/InventoryByProduct/{0}", id);

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
