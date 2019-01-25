using System.Net.Http;
using System.Threading.Tasks;
using OnLineStore.WebAPI.Clients.Models;

namespace OnLineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public interface IRothschildHouseClient
    {
        Task<HttpResponseMessage> PostPaymentAsync(PostPaymentRequest request);
    }
#pragma warning restore CS1591
}
