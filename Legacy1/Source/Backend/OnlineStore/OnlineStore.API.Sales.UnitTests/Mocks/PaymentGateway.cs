using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using OnlineStore.API.Common.Clients.Contracts;
using OnlineStore.API.Common.Clients.Models;

namespace OnlineStore.API.Sales.UnitTests.Mocks
{
    public class MockedRothschildHouseIdentityClient : IRothschildHouseIdentityClient
    {
#pragma warning disable CS1998
        public async Task<TokenResponse> GetRothschildHouseTokenAsync()
            => new RothschildHouseClientMockedTokenResponse();
    }

    public class MockedRothschildHousePaymentClient : IRothschildHousePaymentClient
    {
        public async Task<HttpResponseMessage> PostPaymentAsync(TokenResponse token, PostPaymentRequest request)
            => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(PaymentResponseMocks.SuccessPayment))
            };
    }
#pragma warning restore CS1998

    public static class PaymentResponseMocks
    {
        public static PaymentResponse SuccessPayment
            => new PaymentResponse
            {
                ConfirmationID = Guid.NewGuid(),
                PaymentDateTime = DateTime.Now,
                Last4Digits = "1145"
            };
    }

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
