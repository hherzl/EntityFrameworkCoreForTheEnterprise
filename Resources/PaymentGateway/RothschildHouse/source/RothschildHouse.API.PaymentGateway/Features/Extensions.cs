using System.Reflection;

namespace RothschildHouse.API.PaymentGateway.Features
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
