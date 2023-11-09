namespace RothschildHouse.Application.Queue.Messages;

public record PublishTransactionResult
{
    public string Message { get; set; }
    public bool Failed { get; set; }

    public string Id { get; set; }
}
