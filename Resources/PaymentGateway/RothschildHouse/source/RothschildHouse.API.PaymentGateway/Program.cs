using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using RothschildHouse._3P.CityBank;
using RothschildHouse.API.PaymentGateway;
using RothschildHouse.API.PaymentGateway.Features;
using RothschildHouse.API.PaymentGateway.Filters;
using RothschildHouse.API.PaymentGateway.Hubs;
using RothschildHouse.API.PaymentGateway.Infrastructure;
using RothschildHouse.Library.Common.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<DbTransactionFilter>();

builder
    .Services
    .AddControllers(options =>
    {
        //options.Filters.Add<DbTransactionFilter>();
    })
    ;

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:37200";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.SearchPaymentTransactions, builder => builder.RequireRole(Roles.Admin));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "RothschildHouse.API.PaymentGateway.xml"));
});

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCityBankServices();

builder.Services.AddApplicationServices();

builder
    .Services.AddResponseCompression(opts =>
    {
        opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
    })
    ;

builder
    .Services
    .AddSignalR(options => options.EnableDetailedErrors = true)
    ;

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PaymentTransactionsHub>("/paymenttxnhub");

app.Run();
