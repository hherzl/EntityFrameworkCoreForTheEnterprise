using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Application.Core.Services;

namespace RothschildHouse.Application.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<ReportsService>();

            return services;
        }
    }
}
