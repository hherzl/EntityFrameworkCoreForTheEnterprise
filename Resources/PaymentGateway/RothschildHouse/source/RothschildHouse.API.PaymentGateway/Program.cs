using RothschildHouse.Application;
using RothschildHouse.Application.Queue;
using RothschildHouse.Infrastructure;
using RothschildHouse.Clients.CityBank;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCityBankServices();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("GuiCorsPolicy", builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("https://localhost:7255", "http://localhost:7255");
    });
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
