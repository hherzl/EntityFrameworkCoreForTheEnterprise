using RothschildHouse.Identity.Data.Initializers;

namespace RothschildHouse.Identity;

public static class SeedData
{
    public static async Task EnsureSeedDataAsync(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var dbInitializer = scope.ServiceProvider.GetService<RothschildHouseDbInitializer>();

        await dbInitializer.InitializeAsync();
    }
}
