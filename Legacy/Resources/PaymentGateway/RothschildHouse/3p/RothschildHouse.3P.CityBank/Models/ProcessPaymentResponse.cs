﻿using System.Text.Json;

namespace RothschildHouse._3P.CityBank.Models
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
            => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
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
