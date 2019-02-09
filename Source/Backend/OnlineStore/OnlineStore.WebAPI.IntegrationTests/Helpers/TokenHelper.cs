using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using OnlineStore.WebAPI.IntegrationTests.Mocks;

namespace OnlineStore.WebAPI.IntegrationTests.Helpers
{
    public static class TokenHelper
    {
        public static async Task<TokenResponse> GetOnlineStoreCustomerTokenAsync(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var settings = ClientSettingsMocker.GetOnlineStoreIdentityClientSettings(userName, password);

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

        public static async Task<TokenResponse> GetOnlineStoreCustomerTokenForWolverineAsync()
            => await GetOnlineStoreCustomerTokenAsync("jameslogan@walla.com", "wolverine");
    }
}
