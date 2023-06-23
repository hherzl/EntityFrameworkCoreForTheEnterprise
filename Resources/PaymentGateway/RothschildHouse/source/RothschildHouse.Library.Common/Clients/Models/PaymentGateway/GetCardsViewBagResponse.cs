using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Library.Common.Clients.Models.Common;

namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCardsViewBagResponse : Response
    {
        public List<VCardType> CardTypes { get; set; }        
    }
}
