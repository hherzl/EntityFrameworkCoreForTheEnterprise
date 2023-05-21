﻿using MediatR;
using RothschildHouse.Domain.Core.Common;
using RothschildHouse.Infrastructure.Core.Persistence;

namespace RothschildHouse.Infrastructure.Core
{
    public static class MediatRExtensions
    {
        public static async Task DispatchNotifications(this IMediator mediator, RothschildHouseDbContext dbContext)
        {
            var entities = dbContext
                    .ChangeTracker
                    .Entries<Entity>()
                    .Where(e => e.Entity.Notifications.Any())
                    .Select(e => e.Entity)
                    .ToList()
                    ;

            foreach (var entity in entities)
            {
                foreach (var notification in entity.Notifications)
                    await mediator.Publish(notification);

                entity.ClearNotifications();
            }
        }
    }
}
