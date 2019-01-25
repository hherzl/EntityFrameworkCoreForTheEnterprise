using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Common
{
    public static class LoggingHelper
    {
        public static ILogger<T> GetLogger<T>(bool addTrace = true, bool addDebug = true, bool addInformation = true, bool addWarning = true, bool addError = true, bool addCritical = true)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var service = serviceProvider
                .GetService<ILoggerFactory>();

            if (addTrace)
                service.AddConsole(LogLevel.Trace);

            if (addDebug)
                service.AddConsole(LogLevel.Debug);

            if (addInformation)
                service.AddConsole(LogLevel.Information);

            if (addWarning)
                service.AddConsole(LogLevel.Warning);

            if (addError)
                service.AddConsole(LogLevel.Error);

            if (addCritical)
                service.AddConsole(LogLevel.Critical);

            return serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<T>();
        }
    }
}
