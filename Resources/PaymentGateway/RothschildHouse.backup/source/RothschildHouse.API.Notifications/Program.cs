using RothschildHouse.API.Notifications.Hubs;
using RothschildHouse.API.Notifications.Services;
using RothschildHouse.Library.Common.Queue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("GuiCorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.Configure<MqClientSettings>(builder.Configuration.GetSection("Queue:Transaction"));
builder.Services.AddHostedService<TransactionReceiverService>();

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
app.UseCors("GuiCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<TransactionsHub>("/txnhub");

app.Run();
