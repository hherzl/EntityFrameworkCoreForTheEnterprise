using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnlineStore.WebAPI.IntegrationTests.Helpers
{
    public static class TokenHelper
    {
        public static async Task<TokenResponse> GetCustomerTokenAsync()
        {
            var client = new HttpClient();

            // todo: Get identity server url from config file

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:56000");

            // todo: Get token request from config file

            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoresecret1",
                UserName = "jameslogan@walla.com",
                Password = "wolverine"
            });
        }
    }
}
