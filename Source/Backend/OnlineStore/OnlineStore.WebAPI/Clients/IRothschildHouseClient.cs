using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public interface IRothschildHouseClient
    {
        Task<HttpResponseMessage> PostPaymentAsync(PostPaymentRequest request);
    }
#pragma warning restore CS1591
}
