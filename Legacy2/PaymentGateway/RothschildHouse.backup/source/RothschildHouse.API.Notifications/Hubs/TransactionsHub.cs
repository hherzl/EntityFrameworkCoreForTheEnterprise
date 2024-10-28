using Microsoft.AspNetCore.SignalR;

namespace RothschildHouse.API.Notifications.Hubs
{
    public class TransactionsHub : Hub
    {
        [HubMethodName(HubMethods.SendTxn)]
        public async Task SendTxn(string clientApplication, decimal amount, string currency)
            => await Clients.All.SendAsync(HubMethods.ReceiveTxn, clientApplication, amount, currency);
    }
}
