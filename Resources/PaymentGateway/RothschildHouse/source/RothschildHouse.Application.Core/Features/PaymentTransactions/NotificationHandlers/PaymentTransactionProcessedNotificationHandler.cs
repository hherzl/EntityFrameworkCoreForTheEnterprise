using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RothschildHouse.Application.Core.Hubs;
using RothschildHouse.Domain.Core.Notifications;

namespace RothschildHouse.Application.Core.Features.PaymentTransactions.NotificationHandlers
{
    public class PaymentTransactionProcessedNotificationHandler : INotificationHandler<PaymentTransactionProcessedNotification>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<PaymentTransactionsHub> _hubContext;

        public PaymentTransactionProcessedNotificationHandler(ILogger<PaymentTransactionProcessedNotificationHandler> logger, IHubContext<PaymentTransactionsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(PaymentTransactionProcessedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Payment Transaction processed, GUID: {notification.Guid}, Client application: {notification.ClientApplication} Amount: {notification.Amount} {notification.Currency}");

            await _hubContext.Clients.All.SendAsync(HubMethods.ReceivePaymentTxn, notification.ClientApplication, notification.Amount, notification.Currency);
        }
    }
}
