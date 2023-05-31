using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetPaymentTransactionsViewBagRespose
    {
        public List<ListItem<short?>> PaymentTransactionStatuses { get; set; }
        public List<ListItem<Guid?>> ClientApplications { get; set; }
    }
}
