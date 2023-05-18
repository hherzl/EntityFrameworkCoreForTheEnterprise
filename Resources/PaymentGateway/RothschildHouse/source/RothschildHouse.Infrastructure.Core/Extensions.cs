using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Infrastructure.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RothschildHouseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RothschildHouse")));

            services.AddScoped<IRothschildHouseDbContext, RothschildHouseDbContext>();

            return services;
        }
    }
}
