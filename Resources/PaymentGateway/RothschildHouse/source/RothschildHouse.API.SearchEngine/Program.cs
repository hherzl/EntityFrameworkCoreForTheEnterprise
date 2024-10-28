using RothschildHouse.API.SearchEngine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SaleServiceSettings>(builder.Configuration.GetSection("NoSql:SearchEngine"));
builder.Services.AddScoped<SaleService>();

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
