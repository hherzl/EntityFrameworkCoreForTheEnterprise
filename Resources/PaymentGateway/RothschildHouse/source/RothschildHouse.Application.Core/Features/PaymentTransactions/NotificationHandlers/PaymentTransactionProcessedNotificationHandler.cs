using MediatR;
using Microsoft.Extensions.Logging;
using RothschildHouse.Domain.Core.Notifications;
using RothschildHouse.Library.Common.Queue;
using RothschildHouse.Library.Common.Queue.Messages;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.NotificationHandlers
{
    public class PaymentTransactionProcessedNotificationHandler : INotificationHandler<PaymentTransactionProcessedNotification>
    {
        private readonly ILogger _logger;
        private readonly PaymentTransactionMqClient _paymentTransactionMqClient;

        public PaymentTransactionProcessedNotificationHandler(ILogger<PaymentTransactionProcessedNotificationHandler> logger, PaymentTransactionMqClient paymentTransactionMqClient)
        {
            _logger = logger;
            _paymentTransactionMqClient = paymentTransactionMqClient;
        }

        public Task Handle(PaymentTransactionProcessedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Payment Transaction processed, GUID: {notification.Guid}, Client application: {notification.ClientApplication} Amount: {notification.Amount} {notification.Currency}");

            _paymentTransactionMqClient.Publish(new PublishPaymentTransactionMessage
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
