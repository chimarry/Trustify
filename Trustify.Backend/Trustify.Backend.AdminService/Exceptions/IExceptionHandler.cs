using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Exceptions
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
