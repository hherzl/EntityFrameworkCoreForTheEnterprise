﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RothschildHouse.API.SearchEngine.Services.Models
{
    public class SaleDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("paymentTxnId")]
        public long? PaymentTxnId { get; set; }

        [BsonElement("paymentTxnGuid")]
        public Guid? PaymentTxnGuid { get; set; }

        [BsonElement("clientApplicationId")]
        public Guid? ClientApplicationId { get; set; }

        [BsonElement("clientApplication")]
        public string ClientApplication { get; set; }

        [BsonElement("issuingNetwork")]
        public string IssuingNetwork { get; set; }

        [BsonElement("cardTypeId")]
        public short? CardTypeId { get; set; }

        [BsonElement("cardType")]
        public string CardType { get; set; }

        [BsonElement("total")]
        public double? Total { get; set; }

        [BsonElement("currencyId")]
        public short? CurrencyId { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

        [BsonElement("paymentTxnDateTime")]
        public DateTime? PaymentTxnDateTime { get; set; }

        [BsonElement("createdOn")]
        public DateTime? CreatedOn { get; set; }
    }
}