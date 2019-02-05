using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public class RothschildHouseClient : IRothschildHouseClient
    {
        private readonly HttpClient client;
        private readonly ApiUrl apiUrl;

        public RothschildHouseClient()
        {
            client = new HttpClient();
            apiUrl = new ApiUrl(port: 19000);
        }

        public async Task<HttpResponseMessage> PostPaymentAsync(PostPaymentRequest request)
        {
            var token = await IdentityServerHelper.GetRothschildHouseTokenAsync();

            client.SetBearerToken(token.AccessToken);

            return await client.PostAsync(
                apiUrl.Controller("Transaction").Action("Payment").ToString(),
                request.GetStringContent()
                );
        }
    }
#pragma warning restore CS1591
}
