using MediatR;
using Microsoft.Extensions.Logging;
using RothschildHouse.Application.Queue;
using RothschildHouse.Application.Queue.Messages;
using RothschildHouse.Domain.Notifications;

namespace RothschildHouse.Application.Features.Transactions.NotificationHandlers;

public class TransactionProcessedNotificationHandler : INotificationHandler<TransactionProcessedNotification>
{
    private readonly ILogger _logger;
    private readonly TransactionMqClient _transactionMqClient;

    public TransactionProcessedNotificationHandler(ILogger<TransactionProcessedNotificationHandler> logger, TransactionMqClient transactionMqClient)
    {
        _logger = logger;
        _transactionMqClient = transactionMqClient;
    }

    public Task Handle(TransactionProcessedNotification notification, CancellationToken cancellationToken)
    {
        var messages = new
        {
            log = $"Transaction processed, GUID: {notification.Guid}, Client application: {notification.ClientApplication} Amount: {notification.Amount} {notification.Currency}"
        };

        _logger.LogInformation(messages.log);

        _transactionMqClient.Publish(new PublishTransactionMessage
        {
            Id = notification.Id,
            Guid = notification.Guid,
            ClientApplication = notification.ClientApplication,
            Amount = notification.Amount,
            Currency = notification.Currency
        });

        return Task.CompletedTask;
    }
}
