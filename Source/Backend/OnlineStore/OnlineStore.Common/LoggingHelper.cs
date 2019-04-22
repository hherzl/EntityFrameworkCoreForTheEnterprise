using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Common
{
    public static class LoggingHelper
    {
        public static ILogger<T> GetLogger<T>(bool addTrace = true, bool addDebug = true, bool addInformation = true, bool addWarning = true, bool addError = true, bool addCritical = true)
        {
            return new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<T>();
        }
    }
}
