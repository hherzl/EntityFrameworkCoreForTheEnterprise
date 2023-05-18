using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RothschildHouse.Application.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
