namespace RothschildHouse.Application.Core.Features.PaymentTransactions.Queries
{
    public record PaymentTransactionItemModel
    {
        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CardId { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }
}
