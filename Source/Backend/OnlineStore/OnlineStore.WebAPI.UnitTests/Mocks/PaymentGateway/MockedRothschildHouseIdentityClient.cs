using System.Threading.Tasks;
using IdentityModel.Client;
using OnlineStore.WebAPI.Clients.Contracts;

namespace OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway
{
    public class MockedRothschildHouseIdentityClient : IRothschildHouseIdentityClient
    {
#pragma warning disable CS1998
        public async Task<TokenResponse> GetRothschildHouseTokenAsync()
            => new RothschildHouseClientMockedTokenResponse();
#pragma warning restore CS1998
    }
}
