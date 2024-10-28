using MediatR;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Domain.Core.Notifications
{
    public class CardCreationNotification : INotification
    {
        public CardCreationNotification(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
