using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnlineStore.API.Common.Clients
{
#pragma warning disable CS1591
    public static class IdentityServerHelper
    {
        public static async Task<TokenResponse> GetRothschildHouseTokenAsync(RothschildHouseIdentitySettings settings)
        {
            using (var client = new HttpClient())
            {
                var disco = await client.GetDiscoveryDocumentAsync(settings.Url);

                return await client.RequestPasswordTokenAsync(
                    new PasswordTokenRequest
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = settings.ClientId,
                        ClientSecret = settings.ClientSecret,
                        UserName = settings.UserName,
                        Password = settings.Password
                    });
            }
        }
#pragma warning restore CS1591
    }
}
