using System.Net;
using IdentityModel.Client;

namespace OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway
{
    public class RothschildHouseClientMockedTokenResponse : TokenResponse
    {
        public RothschildHouseClientMockedTokenResponse()
            : base(HttpStatusCode.OK, "mocks", "token")
        {
        }

        public new bool IsError
            => false;
    }
}
