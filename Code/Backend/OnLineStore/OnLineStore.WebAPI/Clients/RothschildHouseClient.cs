using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using OnLineStore.WebAPI.Clients.Models;

namespace OnLineStore.WebAPI.Clients
{
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
                ClientId = "rothschildhousecustomerclient",
                ClientSecret = "rothschildhousesecret1",
                UserName = "",
                Password = ""
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class RothschildHouseClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetHttpClient()
            => new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostPaymentAsync(this HttpClient httpClient, PostPaymentRequest request)
        {
            return await httpClient.PostAsync("http://localhost:19000/api/v1/Transaction/Payment", GetStringContent(request));
        }
    }
}
