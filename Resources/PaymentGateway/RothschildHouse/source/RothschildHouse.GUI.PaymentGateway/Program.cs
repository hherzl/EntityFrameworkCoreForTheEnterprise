using System.Globalization;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RothschildHouse.GUI.PaymentGateway;
using RothschildHouse.Library.Common.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddHttpClient<ReportsClient>(ReportsClient.ClientName, client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>($"Clients:{ReportsClient.ClientName}"));
});

builder.Services.AddHttpClient<PaymentGatewayClient>(PaymentGatewayClient.ClientName, client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>($"Clients:{PaymentGatewayClient.ClientName}"));
});

builder.Services.AddScoped<ReportsClient>();
builder.Services.AddScoped<PaymentGatewayClient>();

var culture = new CultureInfo("en-US");

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();
