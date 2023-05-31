namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record CardItemModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string Last4Digits { get; set; }
        public string ExpirationDate { get; set; }
    }
}
