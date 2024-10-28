using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Application.Common.Contracts;
using RothschildHouse.Infrastructure.Persistence;

namespace RothschildHouse.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RothschildHouseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RothschildHouse")));

        services.AddScoped<IRothschildHouseDbContext, RothschildHouseDbContext>();

        return services;
    }
}
