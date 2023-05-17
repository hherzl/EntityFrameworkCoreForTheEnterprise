using MediatR;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Infrastructure.Persistence
{
#pragma warning disable CS1591
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
