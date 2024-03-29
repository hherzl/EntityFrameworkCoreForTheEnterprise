using RothschildHouse.Application.Core;
using RothschildHouse.Infrastructure.Core;
using RothschildHouse.Library.Common.Queue;
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

builder.Services.AddHttpClient();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCityBankServices();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("GuiCorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.Configure<MqClientSettings>(builder.Configuration.GetSection("Queue:Transaction"));
builder.Services.AddScoped<TransactionMqClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("GuiCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
