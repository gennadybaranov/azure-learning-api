using System;
using NLog;

namespace Itechart.Common.Logging
{
    [UsedImplicitly]
    public class NlogLogger : ILogger
    {
        private readonly Logger _logger;


        public NlogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }


        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message, Exception exception = null)
        {
            if (exception != null)
            {
                _logger.Warn(exception, message);
            }
            else
            {
                _logger.Warn(message);
            }
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void LogFatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }
    }
}