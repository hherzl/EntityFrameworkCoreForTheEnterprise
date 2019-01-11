using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnLineStore.WebAPI.IntegrationTests.Helpers
{
    public static class IdentityServerHelper
    {
        public static async Task<TokenResponse> GetValidCustomerTokenAsync()
        {
            var client = new HttpClient();

            // todo: get identity server url from config file
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:56000").GetAwaiter().GetResult();

            // todo: get token request from config file
            return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoresecret1",
                UserName = "charlesxavier@gmail.com",
                Password = "professorx"
            });
        }
    }
}
