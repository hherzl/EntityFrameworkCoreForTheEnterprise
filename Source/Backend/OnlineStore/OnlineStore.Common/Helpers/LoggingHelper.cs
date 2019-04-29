using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Common.Helpers
{
    public static class LoggingHelper
    {
        public static ILogger<T> GetLogger<T>()
            => new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<T>();
    }
}
