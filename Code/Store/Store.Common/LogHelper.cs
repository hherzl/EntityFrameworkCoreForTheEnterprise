using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Store.Common
{
    public static class LogHelper
    {
        public static ILogger<T> GetLogger<T>()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug)
                .AddConsole(LogLevel.Trace)
                .AddConsole(LogLevel.Information)
                .AddConsole(LogLevel.Warning)
                .AddConsole(LogLevel.Critical)
                .AddConsole(LogLevel.Error);

            return serviceProvider.GetService<ILoggerFactory>().CreateLogger<T>();
        }
    }
}
