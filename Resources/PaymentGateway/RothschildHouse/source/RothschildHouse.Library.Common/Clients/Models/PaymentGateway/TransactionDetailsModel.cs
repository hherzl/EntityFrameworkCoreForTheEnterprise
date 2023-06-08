namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record TransactionDetailsModel
    {
        public TransactionDetailsModel()
        {
            Logs = new();
        }

        public long? Id { get; set; }
        public short? TransactionStatusId { get; set; }
        public string TransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public Guid? CustomerId { get; set; }
        public string Customer { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public List<TransactionLogDetailsModel> Logs { get; set; }
    }
}
