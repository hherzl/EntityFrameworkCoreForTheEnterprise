using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using OnlineStore.API.Common.IntegrationTests.Mocks;

namespace OnlineStore.API.Common.IntegrationTests.Helpers
{
    public static class TokenHelper
    {
        public static async Task<TokenResponse> GetOnlineStoreTokenAsync(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var settings = ClientSettingsMocker.GetIdentityClientSettings(userName, password);

                var disco = await client.GetDiscoveryDocumentAsync(settings.Url);

                return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = settings.ClientId,
                    ClientSecret = settings.ClientSecret,
                    UserName = settings.UserName,
                    Password = settings.Password
                });
            }
        }

        public static async Task<TokenResponse> GetOnlineStoreTokenForWarehouseManagerAsync()
            => await GetOnlineStoreTokenAsync("warehousemanager1@onlinestore.com", "password1");

        public static async Task<TokenResponse> GetTokenForWarehouseOperatorAsync()
            => await GetOnlineStoreTokenAsync("warehouseoperator1@onlinestore.com", "password1");

        public static async Task<TokenResponse> GetTokenForWolverineAsync()
            => await GetOnlineStoreTokenAsync("jameslogan@walla.com", "wolverine");
    }
}
