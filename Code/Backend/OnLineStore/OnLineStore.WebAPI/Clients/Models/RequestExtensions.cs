using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace OnlineStore.WebAPI.Clients.Models
{
#pragma warning disable CS1591
    public static class RequestExtensions
    {
        public static StringContent GetStringContent(this IRequest obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
#pragma warning restore CS1591
}
