using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCustomersViewBagRespose : Response
    {
        public List<ListItem<short?>> Countries { get; set; }
    }
}
