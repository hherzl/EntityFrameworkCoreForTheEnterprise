using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RothschildHouse.GUI.PaymentGateway;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:12001";
    //options.ProviderOptions.ClientId = "ulab.gui.payment-gateway";
    options.ProviderOptions.ClientId = "interactive.mvc.sample";
    options.ProviderOptions.ResponseType = "code";
    //options.ProviderOptions.RedirectUri = "https://localhost:7255";
    
});

await builder.Build().RunAsync();
