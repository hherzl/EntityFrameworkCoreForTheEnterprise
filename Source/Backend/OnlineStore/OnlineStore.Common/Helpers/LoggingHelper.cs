using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Common.Helpers
{
    public static class LoggingHelper
    {
        public static ILogger<TModel> GetLogger<TModel>()
            => new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<TModel>();
    }
}
