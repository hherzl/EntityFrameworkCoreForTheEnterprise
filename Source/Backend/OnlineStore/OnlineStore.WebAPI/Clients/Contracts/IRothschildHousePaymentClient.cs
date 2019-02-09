using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.Clients.Contracts
{
#pragma warning disable CS1591
    public interface IRothschildHousePaymentClient
    {
        Task<HttpResponseMessage> PostPaymentAsync(TokenResponse token, PostPaymentRequest request);
    }
#pragma warning restore CS1591
}
