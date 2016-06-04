﻿using System.Text;
using System.Web.Http.ExceptionHandling;
using NLog;
using System.Net.Http;

namespace fileHandler.api
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Log(LogLevel.Error, RequestToString(context.Request), context.Exception);
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
                message.Append(request.Method);

            if (request.RequestUri != null)
                message.Append(" ").Append(request.RequestUri);

            return message.ToString();
        }
    }
}
