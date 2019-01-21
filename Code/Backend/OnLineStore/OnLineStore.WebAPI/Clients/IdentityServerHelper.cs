using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnLineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public static class IdentityServerHelper
    {
        public static async Task<TokenResponse> GetValidCustomerTokenAsync()
        {
            var client = new HttpClient();

            // todo: get identity server url from config file

            var disco = client.GetDiscoveryDocumentAsync("http://localhost:18000").GetAwaiter().GetResult();

            // todo: get token request from config file

            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoreclientsecret1",
                UserName = "administrator@onlinestore.com",
                Password = "onlinestore1"
            });
        }
#pragma warning restore CS1591
    }
}
