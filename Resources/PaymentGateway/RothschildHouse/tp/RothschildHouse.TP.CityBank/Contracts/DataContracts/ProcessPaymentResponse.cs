using System.Text.Json;

namespace RothschildHouse.TP.CityBank.Contracts.DataContracts
{
    public record ProcessPaymentResponse
    {
        public ProcessPaymentResponse()
        {
            Authorization = new ProcessPaymentAuthorization();
            Capture = new ProcessPaymentCapture();
        }

        public bool Successed { get; set; }
        public Guid? Guid { get; set; }

        public string AvsPayload { get; set; }
        public string Cvv2Payload { get; set; }

        public ProcessPaymentAuthorization Authorization { get; set; }
        public ProcessPaymentCapture Capture { get; set; }

        public string SubscriptionTransactionId { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this, GlobalJsonSerializerOptions.Default);
    }

    public record ProcessPaymentAuthorization
    {
        public string TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionPayload { get; set; }
    }

    public record ProcessPaymentCapture
    {
        public string TransactionId { get; set; }
        public string TransactionPayload { get; set; }
    }
}
