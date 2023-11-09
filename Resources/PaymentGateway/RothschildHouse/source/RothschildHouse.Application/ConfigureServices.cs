using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Application.Clients.SearchEngine;

namespace RothschildHouse.Application;

public static class ConfigureServices
{
    const string APPLICATION_JSON = "application/json";

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddHttpClient<SearchEngineClient>("SearchEngine", client =>
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPLICATION_JSON));
            client.BaseAddress = new Uri(configuration["Clients:SearchEngine"]);
        });

        services.AddScoped<SearchEngineClient>();

        return services;
    }
}
