using System;

namespace Itechart.Common.Logging
{
    public interface ILogger
    {
        void LogTrace(string message);

        void LogDebug(string message);

        void LogInfo(string message);

        void LogWarning(string message, Exception exception = null);

        void LogError(string message, Exception exception);

        void LogFatal(string message, Exception exception);
    }
}