using System.Globalization;
using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public (OperationStatus status, string detailedMessage) HandleException(Exception exception)
        {
            logger.LogError("Exception with {message}",exception.Message);
            return Handle(exception);
        }

        public (OperationStatus status, string detailedMessage) HandleException(Exception exception, object details)
        {
            logger.LogError("Exception with {message}", $"{exception.Message}  {details}");
            return Handle(exception);
        }

        private static (OperationStatus status, string detailedMessage) Handle(Exception exception)
        {
            if (exception is InvalidOperationException invalidOperationException)
                return (OperationStatus.UnknownError, invalidOperationException.Message);
            else if (exception is IOException iOException)
                return (OperationStatus.FileSystemError, iOException.Message);
            else if (exception is AppNotConfiguredException notConfiguredException)
                return (OperationStatus.InvalidData, notConfiguredException.Message);
            else if (exception is TaskCanceledException taskCanceledException)
                return (OperationStatus.Success, taskCanceledException.Message);
            else if (exception.Message.Contains("Call failed with status code 403"))
                return (OperationStatus.ForbiddenAccess, exception.Message);
            else if(exception.Message.Contains("Call failed with status code 401"))
                return (OperationStatus.Unauthorized, exception.Message);
            return (OperationStatus.UnknownError, exception.Message);
        }
    }
}
