using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record SearchPaymentTransactionsViewBagRespose : Response
    {
        public List<ListItem<short>> PaymentTransactionStatuses { get; set; }
        public List<ListItem<Guid?>> ClientApplications { get; set; }
    }
}
