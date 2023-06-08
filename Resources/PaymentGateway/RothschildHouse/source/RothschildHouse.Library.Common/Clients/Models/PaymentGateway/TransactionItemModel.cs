namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record TransactionItemModel
    {
        public long? Id { get; set; }
        public short? TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public short? TransactionStatusId { get; set; }
        public string TransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CardId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardNumber { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }
}
