using RothschildHouse.Application.Core;
using RothschildHouse.Application.Core.Hubs;
using RothschildHouse.Infrastructure.Core;
using RothschildHouse.TP.CityBank;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "RothschildHouse.API.PaymentGateway.xml"));
});

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCityBankServices();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("RothschildHouseCorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder
    .Services
    .AddSignalR(options => options.EnableDetailedErrors = true)
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("RothschildHouseCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<PaymentTransactionsHub>("/paymenttxnhub");

app.Run();
