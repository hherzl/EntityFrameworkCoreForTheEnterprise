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

builder.Services.AddHttpClient<ReportsClient>("Reports", client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Clients:Reports"));
});

builder.Services.AddHttpClient<PaymentGatewayClient>("PaymentGateway", client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.ApplicationJson));
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Clients:PaymentGateway"));
});

builder.Services.AddScoped<ReportsClient>();
builder.Services.AddScoped<PaymentGatewayClient>();

await builder.Build().RunAsync();
