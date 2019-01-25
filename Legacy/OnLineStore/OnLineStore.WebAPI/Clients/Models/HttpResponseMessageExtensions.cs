using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnLineStore.WebAPI.Clients.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static async Task<PaymentResponse> GetPaymentResponseAsync(this HttpResponseMessage responseMessage)
        {
            var json = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PaymentResponse>(json);
        }
    }
}
