using Microsoft.AspNetCore.SignalR;

namespace RothschildHouse.API.Notifications.Hubs
{
    public class PaymentTransactionsHub : Hub
    {
        [HubMethodName(HubMethods.SendPaymentTxn)]
        public async Task SendPaymentTxn(string clientApplication, decimal amount, string currency)
            => await Clients.All.SendAsync(HubMethods.ReceivePaymentTxn, clientApplication, amount, currency);
    }
}
