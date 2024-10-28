using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace RothschildHouse.Domain.Common;

public abstract class Entity
{
    public bool? Active { get; set; }

    #region [ Notifications ]

    private readonly List<INotification> _notifications = new();

    [NotMapped]
    public IReadOnlyCollection<INotification> Notifications
        => _notifications.AsReadOnly();

    public void AddNotification(INotification notification)
    {
        _notifications.Add(notification);
    }

    public void RemoveNotification(INotification notification)
    {
        _notifications.Remove(notification);
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }

    #endregion
}
