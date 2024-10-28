using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RothschildHouse.Infrastructure.Persistence;

namespace RothschildHouse.Seed.Dbo;

internal static class DbContextHelper
{
    private static readonly IConfigurationRoot _configurationRoot;

    static DbContextHelper()
    {
        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            ;
    }

    private static string ConnectionString
        => _configurationRoot.GetConnectionString("RothschildHouse");

    public static RothschildHouseDbContext GetRothschildHouseDbContext()
        => new(new DbContextOptionsBuilder<RothschildHouseDbContext>().UseSqlServer(ConnectionString).Options, null);
}
