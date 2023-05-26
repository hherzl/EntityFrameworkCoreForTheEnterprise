using RothschildHouse.TP.CityBank.Contracts;
using RothschildHouse.TP.CityBank.Contracts.DataContracts;
using RothschildHouse.TP.CityBank.Data;

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
                return await Task.FromResult(new ProcessPaymentResponse { Successed = false });

            await Task.Delay(2000);

            return await Task.FromResult(new ProcessPaymentResponse
            {
                Successed = true,
                Guid = Guid.NewGuid(),
                AvsPayload = "",
                Cvv2Payload = "",
                Authorization = new ProcessPaymentAuthorization
                {
                    TransactionId = "",
                    TransactionCode = "",
                    TransactionPayload = ""
                },
                Capture = new ProcessPaymentCapture
                {
                    TransactionId = "",
                    TransactionPayload = ""
                },
                SubscriptionTransactionId = ""
            });
        }
    }
}
