namespace RothschildHouse.Library.Common.Queue.Messages
{
    public record PublishPaymentTransactionResult
    {
        public string Message { get; set; }
        public bool Failed { get; set; }

        public string Id { get; set; }
    }
}
