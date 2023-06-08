namespace RothschildHouse.Library.Common.Clients.Models.SearchEngine
{
    public record IndexSaleRequest
    {
        public long? TxnId { get; set; }
        public Guid? TxnGuid { get; set; }
        public DateTime? TxnDateTime { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public string IssuingNetwork { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public double? Total { get; set; }
        public short? CurrencyId { get; set; }
        public string Currency { get; set; }
    }
}
