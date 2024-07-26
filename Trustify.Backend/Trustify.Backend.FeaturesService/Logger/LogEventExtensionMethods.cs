using Serilog.Events;
using Trustify.Backend.FeaturesService.Logger.Models;
using Action = Trustify.Backend.FeaturesService.Logger.Models.Action;

namespace Trustify.Backend.FeaturesService.Logger
{
    public static class LogEventExtensionMethods
    {
        /// <summary>
        /// Gets property from Properties filed in LogEvent object <see cref="LogEvent"/>
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? GetProperty(this LogEvent logEvent, string key)
        {
            if (logEvent.Properties.TryGetValue(key, out LogEventPropertyValue? property))
                return property?.ToString();

            return null;
        }

        public static Action? BuildAction(this LogEvent logEvent)
        {
            if (logEvent.GetProperty("ActionName") == null)
                return null;
            else
                return new Action()
                {
                    ActionId = logEvent.GetProperty("ActionId"),
                    ActionName = logEvent.GetProperty("ActionName"),
                    EnvironmentUserName = logEvent.GetProperty("EnvironmentUserName")
                };
        }

        public static Error BuildError(this LogEvent logEvent)
        {
            return new Error()
            {
                Message = logEvent?.Exception?.Message ?? logEvent?.Exception?.InnerException?.Message,
                StackTrace = logEvent?.Exception?.StackTrace ?? logEvent?.Exception?.InnerException?.StackTrace,
                ExceptionType = logEvent?.Exception?.GetType().FullName
            };
        }

        public static Http? BuildHttp(this LogEvent logEvent)
        {
            if (logEvent.GetProperty(LogContextConstants.Method) == null)
                return null;
            else
                return new Http()
                {
                    Method = logEvent.GetProperty(LogContextConstants.Method),
                    ContentType = logEvent.GetProperty(LogContextConstants.ContentType),
                    RequestPath = logEvent.GetProperty(LogContextConstants.RequestPath),
                    Protocol = logEvent.GetProperty(LogContextConstants.Protocol),
                    QueryString = logEvent.GetProperty(LogContextConstants.QueryString)
                };
        }
    }
}
