using RothschildHouse._3P.CityBank.Contracts;
using RothschildHouse._3P.CityBank.Data;
using RothschildHouse._3P.CityBank.Models;

namespace RothschildHouse._3P.CityBank
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
