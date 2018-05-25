using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests
{
    public class SalesTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public SalesTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestOrderAsync()
        {
            // Arrange
            var request = "/api/v1/Sales/Order";

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCurrencyAsync()
        {
            // Arrange
            var currencyID = (short)1;
            var request = string.Format("/api/v1/Sales/Order?currencyID={0}", currencyID);

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByCustomerAsync()
        {
            // Arrange
            var customerID = (short)1;
            var request = string.Format("/api/v1/Sales/Order?customerID={0}", customerID);

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrdersByEmployeeAsync()
        {
            // Arrange
            var employeeID = (short)1;
            var request = string.Format("/api/v1/Sales/Order?employeeID={0}", employeeID);

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderAsync()
        {
            // Arrange
            var id = 1;
            var request = string.Format("/api/v1/Sales/Order/{0}", id);

            // Act
            var response = await Client.GetAsync(request);
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetNonExistingOrderAsync()
        {
            // Arrange
            var id = 0;
            var request = string.Format("/api/v1/Sales/Order/{0}", id);

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCreateOrderRequestAsync()
        {
            // Arrange
            var request = "/api/v1/Sales/CreateOrderRequest";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCloneOrderAsync()
        {
            // Arrange
            var id = 1;
            var request = string.Format("/api/v1/Sales/CloneOrder/{0}", id);

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
