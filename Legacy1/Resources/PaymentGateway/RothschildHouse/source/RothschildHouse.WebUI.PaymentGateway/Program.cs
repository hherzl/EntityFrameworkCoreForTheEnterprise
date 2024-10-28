using RothschildHouse.Library.Common.Clients.Contracts;
using RothschildHouse.WebUI.PaymentGateway;
using RothschildHouse.WebUI.PaymentGateway.Clients;
using RothschildHouse.WebUI.PaymentGateway.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<IRothschildHouseClient, RothschildHouseClient>();
builder.Services.AddScoped<IRothschildHouseClient, RothschildHouseClient>();

builder.Services.AddLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(SupportedCultures.Items[0])
    .AddSupportedCultures(SupportedCultures.Items)
    .AddSupportedUICultures(SupportedCultures.Items);

app.UseRequestLocalization(localizationOptions);

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
