using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetTransactionsViewBagRespose : Response
    {
        public List<ListItem<short?>> TransactionStatuses { get; set; }
        public List<ListItem<Guid?>> ClientApplications { get; set; }
    }
}
