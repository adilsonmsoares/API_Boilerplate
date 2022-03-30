using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Core.Services.Helpers
{
    public static class LoggingHelper
    {
        public static void LogInformation(ILogger logger, string message, string action)
        {
            using (LogContext.PushProperty("Action", action))
            {
                logger?.LogInformation(message);
            }
        }

        public static void LogError(ILogger logger, string message, string action)
        {
            using (LogContext.PushProperty("Action", action))
            {
                logger?.LogError(message);
            }
        }
    }
}
