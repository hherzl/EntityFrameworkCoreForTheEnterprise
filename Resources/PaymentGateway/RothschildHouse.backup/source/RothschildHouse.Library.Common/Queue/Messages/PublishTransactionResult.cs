namespace RothschildHouse.Library.Common.Queue.Messages
{
    public record PublishTransactionResult
    {
        public string Message { get; set; }
        public bool Failed { get; set; }

        public string Id { get; set; }
    }
}
