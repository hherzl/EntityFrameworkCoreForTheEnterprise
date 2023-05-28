using RothschildHouse.TP.CityBank.Contracts.DataContracts;

namespace RothschildHouse.TP.CityBank.Payloads.ProcessPayment.Mocks
{
    internal class ProcessPaymentAuthorizationMocks
    {
        public static ProcessPaymentAuthorization Mock()
        {
            return new()
            {
                TransactionId = Guid.NewGuid().ToString(),
                TransactionCode = TimeSpan.FromDays(DateTime.Now.TimeOfDay.TotalDays).ToString(),
                TransactionPayload = "{ }"
            };
        }
    }
}
