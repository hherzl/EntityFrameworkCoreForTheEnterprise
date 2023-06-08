using MediatR;
using Microsoft.Extensions.Logging;
using RothschildHouse.Domain.Core.Notifications;
using RothschildHouse.Library.Common.Queue;
using RothschildHouse.Library.Common.Queue.Messages;

namespace RothschildHouse.Application.Core.Features.Transactions.NotificationHandlers
{
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
            _logger.LogInformation($"Transaction processed, GUID: {notification.Guid}, Client application: {notification.ClientApplication} Amount: {notification.Amount} {notification.Currency}");

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
}
