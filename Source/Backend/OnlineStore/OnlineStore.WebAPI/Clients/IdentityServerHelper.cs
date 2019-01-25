using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public static class IdentityServerHelper
    {
        public static async Task<TokenResponse> GetRothschildHouseTokenAsync()
        {
            var client = new HttpClient();

            // todo: Get identity server url from config file

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:18000");

            // todo: Get token request from config file

            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoreclientsecret1",
                UserName = "administrator@onlinestore.com",
                Password = "onlinestore1"
            });
        }

        public static async Task<TokenResponse> GetCustomerTokenAsync(string userName, string password)
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
                UserName = userName,
                Password = password
            });
        }
#pragma warning restore CS1591
    }
}
