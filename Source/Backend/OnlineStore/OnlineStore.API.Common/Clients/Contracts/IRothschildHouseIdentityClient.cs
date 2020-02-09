using System.Threading.Tasks;
using IdentityModel.Client;

namespace OnlineStore.API.Common.Clients.Contracts
{
#pragma warning disable CS1591
    public interface IRothschildHouseIdentityClient
    {
        Task<TokenResponse> GetRothschildHouseTokenAsync();
    }
#pragma warning restore CS1591
}
