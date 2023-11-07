namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record TransactionLogDetailsModel
    {
        public long? Id { get; set; }
        public short? TransactionStatusId { get; set; }
        public string TransactionStatus { get; set; }
        public string LogType { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }
        public DateTime? CreationDateTime { get; set; }
    }
}
