using RothschildHouse.TP.CityBank.Contracts.DataContracts;

namespace RothschildHouse.TP.CityBank.Payloads.ProcessPayment.Mocks
{
    internal class ProcessPaymentCaptureMocks
    {
        public static ProcessPaymentCapture Mock()
        {
            return new()
            {
                TransactionId = Guid.NewGuid().ToString(),
                TransactionPayload = "{ }"
            };
        }
    }
}
