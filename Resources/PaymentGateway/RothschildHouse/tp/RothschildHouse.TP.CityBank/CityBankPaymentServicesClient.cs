using RothschildHouse.TP.CityBank.Contracts;
using RothschildHouse.TP.CityBank.Contracts.DataContracts;
using RothschildHouse.TP.CityBank.Data;
using RothschildHouse.TP.CityBank.Payloads.Avs.Mocks;
using RothschildHouse.TP.CityBank.Payloads.Cvv2.Mocks;
using RothschildHouse.TP.CityBank.Payloads.ProcessPayment.Mocks;

namespace RothschildHouse.TP.CityBank
{
    public class CityBankPaymentServicesClient : ICityBankPaymentServicesClient
    {
        private readonly Database _database;

        public CityBankPaymentServicesClient()
        {
            _database = new();
            _database.Init();
        }

        public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
        {
            var card = _database.Cards.FirstOrDefault(item => item.CardNumber == request.CardNumber);

            if (card == null)
                return await Task.FromResult(ProcessPaymentResponse.Failed);

            await Task.Delay(2000);

            var response = new ProcessPaymentResponse
            {
                Successed = true,
                Guid = Guid.NewGuid(),
                AvsPayload = AvsMocks.StreetAndZip("payment", request).ToJson(),
                Cvv2Payload = Cvv2Mocks.Match().ToJson(),
                Authorization = ProcessPaymentAuthorizationMocks.Mock(),
                Capture = ProcessPaymentCaptureMocks.Mock(),
                SubscriptionTransactionId = ""
            };

            return await Task.FromResult(response);
        }
    }
}
