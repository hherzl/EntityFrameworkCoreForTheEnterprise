using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;

namespace RothschildHouse.API.PaymentGateway.Infrastructure
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RothschildHouseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RothschildHouse")));

            return services;
        }
    }
}
