using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;

namespace Trustify.Backend.FeaturesService.Logger
{
    public class TrustifyLogsFormatter : ITextFormatter
    {
        /// <summary>
        /// Format the log event into the output. Subsequent events will be newline-delimited.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatEvent(logEvent, output);
            output.WriteLine();
        }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public static void FormatEvent(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            string? json = JsonConvert.SerializeObject(TrustifyLogBase.ConvertToBase(logEvent), Formatting.None);

            output.Write(json);
        }
    }
}
