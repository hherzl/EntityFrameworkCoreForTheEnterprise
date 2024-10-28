namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record PaymentTransactionDetailsModel
    {
        public PaymentTransactionDetailsModel()
        {
            Logs = new();
        }

        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public string ClientApplication { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? CreationDateTime { get; set; }

        public List<PaymentTransactionLogDetailsModel> Logs { get; set; }
    }
}
