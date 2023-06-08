using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RothschildHouse.Library.Common.NoSql.Documents
{
    public class SaleDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("txnId")]
        public long? TxnId { get; set; }

        [BsonElement("txnGuid")]
        public Guid? TxnGuid { get; set; }

        [BsonElement("txnDateTime")]
        public DateTime? TxnDateTime { get; set; }

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

        [BsonElement("createdOn")]
        public DateTime? CreatedOn { get; set; }
    }
}
