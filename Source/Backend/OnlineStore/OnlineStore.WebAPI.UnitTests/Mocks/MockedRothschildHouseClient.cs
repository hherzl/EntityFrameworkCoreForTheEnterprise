using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineStore.WebAPI.Clients;
using OnlineStore.WebAPI.Clients.Models;

namespace OnlineStore.WebAPI.UnitTests.Mocks
{
#pragma warning disable CS1998
    public class MockedRothschildHouseClient : IRothschildHouseClient
    {
        public async Task<HttpResponseMessage> PostPaymentAsync(PostPaymentRequest request)
            => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(PaymentMocks.SuccessPayment))
            };
    }
#pragma warning restore CS1998
}
