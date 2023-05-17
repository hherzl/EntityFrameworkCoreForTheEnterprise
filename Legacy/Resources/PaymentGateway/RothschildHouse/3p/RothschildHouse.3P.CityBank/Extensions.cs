using Microsoft.Extensions.DependencyInjection;
using RothschildHouse._3P.CityBank.Contracts;

namespace RothschildHouse._3P.CityBank
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
