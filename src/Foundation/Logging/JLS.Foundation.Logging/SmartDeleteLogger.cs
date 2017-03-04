using log4net;
using Sitecore.Diagnostics;
using System;

namespace JLS.Foundation.Logging
{
    public static class SmartDeleteLogger
    {
        private static readonly ILog _logger = LogManager.GetLogger("JLSFoundationLogger");

        public static void Debug(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _logger.Debug(message);
        }

        public static void Debug(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _logger.Debug(message, exception);
        }

        public static void Info(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _logger.Info(message);
        }

        public static void Info(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _logger.Info(message, exception);
        }

        public static void Warn(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _logger.Warn(message);
        }

        public static void Warn(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _logger.Warn(message, exception);
        }

        public static void Error(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _logger.Error(message);
        }

        public static void Error(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _logger.Error(message, exception);
        }

        public static void Fatal(string message)
        {
            Assert.ArgumentNotNull(message, "message");

            _logger.Fatal(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(exception, "exception");

            _logger.Fatal(message, exception);
        }
    }
}