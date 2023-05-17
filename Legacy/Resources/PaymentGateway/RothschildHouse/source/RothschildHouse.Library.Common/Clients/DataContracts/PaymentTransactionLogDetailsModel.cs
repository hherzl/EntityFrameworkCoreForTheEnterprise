namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record PaymentTransactionLogDetailsModel
    {
        public long? Id { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public string PaymentTransactionStatus { get; set; }
        public string LogType { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }
}
