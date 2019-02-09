using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using OnlineStore.WebAPI.Clients.Contracts;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public class RothschildHousePaymentClient : IRothschildHousePaymentClient
    {
        private readonly RothschildHousePaymentSettings Settings;
        private readonly ApiUrl apiUrl;

        public RothschildHousePaymentClient(IOptions<RothschildHousePaymentSettings> settings)
        {
            Settings = settings.Value;
            apiUrl = new ApiUrl(baseUrl: Settings.Url);
        }

        public async Task<HttpResponseMessage> PostPaymentAsync(TokenResponse token, PostPaymentRequest request)
        {
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);

                return await client.PostAsync(
                    apiUrl.Controller("Transaction").Action("Payment").ToString(),
                    request.GetStringContent()
                    );
            }
        }
    }
#pragma warning restore CS1591
}
