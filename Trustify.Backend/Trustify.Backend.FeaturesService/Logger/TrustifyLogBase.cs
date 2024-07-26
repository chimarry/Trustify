using Newtonsoft.Json;
using Serilog.Events;
using System.Runtime.Serialization;
using Trustify.Backend.FeaturesService.Logger.Models;
using Action = Trustify.Backend.FeaturesService.Logger.Models.Action;

namespace Trustify.Backend.FeaturesService.Logger
{
    /// <summary>
    /// Set of fields for ingesting data into the trustify logs page. A common schema can help to correlate 
    /// data from sources like logs and metrics or security analytics. 
    /// This class will enable user to convert Serilog LogEvent into a class processable by Trustify system.
    /// </summary>
    public class TrustifyLogBase
    {
        /// <summary>
        /// Type or level of the log
        /// </summary>
        [JsonProperty("type")]
        [DataMember(Name = "type")]
        public string? Level { get; set; }

        [JsonProperty("requestId")]
        [DataMember(Name = "requestId")]
        public string? RequestId { get; set; }

        /// <summary>
        /// Timestamp of the log (when it was created)?
        /// </summary>
        [JsonProperty("timestamp")]
        [DataMember(Name = "timestamp")]
        public DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// Message template used for log method
        /// </summary>
        [JsonProperty("messageTemplate")]
        [DataMember(Name = "messageTemplate")]
        public string? MessageTemplate { get; set; }

        /// <summary>
        /// Additional data in form of a string
        /// </summary>
        [JsonProperty("metadata")]
        [DataMember(Name = "metadata")]
        public string? Metadata { get; set; }

        /// <summary>
        /// Information about error, if it happened
        /// </summary>
        [JsonProperty("error")]
        [DataMember(Name = "error")]
        public Error? Error { get; set; }

        /// <summary>
        /// Information about http, if this is the request/response HTTP log
        /// </summary>
        [JsonProperty("http")]
        [DataMember(Name = "http")]
        public Http? Http { get; set; }

        /// <summary>
        /// If there are details about method being called, this will contain it
        /// </summary>
        [JsonProperty("action")]
        [DataMember(Name = "action")]
        public Action? ActionDetails { get; set; }

        [JsonProperty("username")]
        [DataMember(Name = "username")]
        public string? Username { get; set; }

        /// <summary>
        /// Creates object that represents Log entry. LogEvent object is converted to our format.
        /// </summary>
        /// <param name="logEvent">LogEvent object that contains information about log</param>
        /// <returns></returns>
        public static TrustifyLogBase ConvertToBase(LogEvent logEvent)
        {
            var log = new TrustifyLogBase
            {
                Level = logEvent.Level.ToString(),
                Timestamp = logEvent.Timestamp
            };

            if (logEvent.Exception != null)
                log.Error = logEvent.BuildError();

            if (logEvent.MessageTemplate != null)
                log.MessageTemplate = logEvent.MessageTemplate.ToString();

            if (logEvent.Properties != null)
            {
                log.Http = logEvent.BuildHttp();
                log.ActionDetails = logEvent.BuildAction();
                log.RequestId = logEvent.GetProperty("RequestId");
                log.Username = logEvent.GetProperty(LogContextConstants.Username);
                log.Metadata = GetMetadata(logEvent)?.ToString();
            }

            return log;
        }

        public static LogEventPropertyValue? GetMetadata(LogEvent logEvent)
        {
            if (logEvent.Properties != null && logEvent.Properties.TryGetValue("Metadata", out LogEventPropertyValue? property))
                return property;
            return null;
        }
    }
}
