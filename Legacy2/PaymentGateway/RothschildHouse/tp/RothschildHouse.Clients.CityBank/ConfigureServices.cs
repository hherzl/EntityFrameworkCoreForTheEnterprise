using Microsoft.Extensions.DependencyInjection;

namespace RothschildHouse.Clients.CityBank;

public static class ConfigureServices
{
    public static IServiceCollection AddCityBankServices(this IServiceCollection services)
    {
        services.AddScoped<ICityBankPaymentServicesClient, CityBankPaymentServicesClient>();

        return services;
    }
}
