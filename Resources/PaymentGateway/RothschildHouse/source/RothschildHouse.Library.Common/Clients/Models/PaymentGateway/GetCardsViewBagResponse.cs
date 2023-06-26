using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCardsViewBagResponse : Response
    {
        public List<ListItem<long?>> CardTypes { get; set; }        
    }
}
