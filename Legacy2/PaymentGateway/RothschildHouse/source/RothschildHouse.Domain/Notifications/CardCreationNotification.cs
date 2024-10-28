using MediatR;
using RothschildHouse.Domain.Entities;

namespace RothschildHouse.Domain.Notifications;

public class CardCreationNotification : INotification
{
    public CardCreationNotification(Card card)
    {
        Card = card;
    }

    public Card Card { get; }
}
