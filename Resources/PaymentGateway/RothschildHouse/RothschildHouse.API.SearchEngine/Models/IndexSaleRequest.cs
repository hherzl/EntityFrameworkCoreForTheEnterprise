namespace RothschildHouse.API.SearchEngine.Models
{
    public record IndexSaleRequest
    {
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
