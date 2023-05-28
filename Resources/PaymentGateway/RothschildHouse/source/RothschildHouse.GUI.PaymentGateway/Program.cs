using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RothschildHouse.GUI.PaymentGateway;
using RothschildHouse.GUI.PaymentGateway.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddHttpClient();

builder.Services.AddScoped<RothschildHouseClient>();
builder.Services.AddScoped<RothschildHouseReportsClient>();

await builder.Build().RunAsync();
