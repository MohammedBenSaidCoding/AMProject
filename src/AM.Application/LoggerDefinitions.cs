using Microsoft.Extensions.Logging;

namespace AM.Application;

public static partial class LoggerDefinitions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "{exceptionMessage}, {stackTrace}")]
    public static partial void LogException(this ILogger logger, string exceptionMessage, string? stackTrace);
 
}