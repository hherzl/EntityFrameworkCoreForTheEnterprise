using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.TP.CityBank.Contracts;

namespace RothschildHouse.TP.CityBank
{
    public static class Extensions
    {
        public static IServiceCollection AddCityBankServices(this IServiceCollection services)
        {
            services.AddScoped<ICityBankPaymentServicesClient, CityBankPaymentServicesClient>();

            return services;
        }
    }
}
