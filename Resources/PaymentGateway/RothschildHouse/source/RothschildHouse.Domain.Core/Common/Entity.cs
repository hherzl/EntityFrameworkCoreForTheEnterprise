﻿using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using RothschildHouse.Domain.Core.Common.Contracts;

namespace RothschildHouse.Domain.Core.Common
{
    public class Entity : IEntity
    {
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

        public bool? Active { get; set; }
    }
}
