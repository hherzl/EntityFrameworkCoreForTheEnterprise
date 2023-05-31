using RothschildHouse.GUI.PaymentGateway.Clients.Models.Common;

namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.PaymentGateway
{
    public record GetPaymentTransactionsViewBagRespose
    {
        public List<ListItem<short?>> PaymentTransactionStatuses { get; set; }
        public List<ListItem<Guid?>> ClientApplications { get; set; }
    }
}
