using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnlineStore.WebAPI.Clients.Models
{
#pragma warning disable CS1591
    public static class HttpResponseMessageExtensions
    {
        public static async Task<PaymentResponse> GetPaymentResponseAsync(this HttpResponseMessage responseMessage)
        {
            var json = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PaymentResponse>(json);
        }
#pragma warning restore CS1591
    }
}
