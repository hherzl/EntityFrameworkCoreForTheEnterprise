using System.Net.Http;
using System.Threading.Tasks;
using OnLineStore.WebAPI.Clients.Models;

namespace OnLineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public class RothschildHouseClient : IRothschildHouseClient
    {
        private HttpClient client;
        private ApiUrl apiUrl;

        public RothschildHouseClient()
        {
            client = new HttpClient();
            apiUrl = new ApiUrl(port: 19000);
        }

        public async Task<HttpResponseMessage> PostPaymentAsync(PostPaymentRequest request)
        {
            return await client.PostAsync(apiUrl.Controller("Transaction").Action("Payment").ToString(), request.GetStringContent());
        }
    }
#pragma warning restore CS1591
}
