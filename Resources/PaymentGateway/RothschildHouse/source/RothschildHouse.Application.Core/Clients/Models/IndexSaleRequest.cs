namespace RothschildHouse.Application.Core.Clients.Models
{
    public record IndexSaleRequest
    {
        public long? PaymentTxnId { get; set; }
        public Guid? PaymentTxnGuid { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public string IssuingNetwork { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public double? Total { get; set; }
        public short? CurrencyId { get; set; }
        public string Currency { get; set; }
        public DateTime? PaymentTxnDateTime { get; set; }
    }
}
