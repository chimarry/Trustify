using Trustify.Backend.FeaturesCore.AutoMapper;

namespace Trustify.Backend.FeaturesCore.Services
{
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handles exception.
        /// </summary>
        /// <param name="exception">Exception to handle.</param>
        /// <returns></returns>
        (OperationStatus status, string detailedMessage) HandleException(Exception exception);

        /// <summary>
        /// Handles exception.
        /// </summary>
        /// <param name="exception">Exception to handle.</param>
        /// <returns></returns>
        (OperationStatus status, string detailedMessage) HandleException(Exception exception, object details);
    }
}
