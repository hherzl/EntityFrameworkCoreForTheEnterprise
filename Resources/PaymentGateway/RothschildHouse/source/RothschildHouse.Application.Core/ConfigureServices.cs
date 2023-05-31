using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Library.Common.Clients;

namespace RothschildHouse.Application.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddHttpClient<SearchEngineClient>("SearchEngine", client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
                client.BaseAddress = new Uri(configuration["Clients:SearchEngine"]);
            });

            services.AddScoped<SearchEngineClient>();

            return services;
        }
    }
}
