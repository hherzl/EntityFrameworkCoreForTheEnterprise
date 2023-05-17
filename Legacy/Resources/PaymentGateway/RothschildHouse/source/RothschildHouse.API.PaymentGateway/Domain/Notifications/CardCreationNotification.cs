using MediatR;
using RothschildHouse.API.PaymentGateway.Domain.Entities;

namespace RothschildHouse.API.PaymentGateway.Domain.Notifications
{
#pragma warning disable CS1591
    public class CardCreationNotification : INotification
    {
        public CardCreationNotification(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
