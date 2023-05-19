namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record CurrencyItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
    }
}
