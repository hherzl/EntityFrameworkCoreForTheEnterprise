using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using OnlineStore.WebAPI.Clients.Contracts;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.UnitTests.Mocks.PaymentGateway
{
#pragma warning disable CS1998
    public class MockedRothschildHousePaymentClient : IRothschildHousePaymentClient
    {
        public async Task<HttpResponseMessage> PostPaymentAsync(TokenResponse token, PostPaymentRequest request)
            => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(PaymentResponseMocks.SuccessPayment))
            };
    }
#pragma warning restore CS1998
}
