using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using Trustify.Backend.AdminService.Exceptions;

namespace Trustify.Backend.AdminService.Middlewares
{
    /// <summary>
    /// Capture synchronous and asynchronous exceptions from the HTTP pipeline and generate error responses. 
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private const string BriefExceptionMessage = "The requested action was not successful.";

        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
             => (this.next, this.logger) = (next, logger);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
           HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // Specify different custom exceptions here
            if (ex is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (ex is HttpRequestException)
            {
                code = HttpStatusCode.FailedDependency;
            }
            else if(ex is ForbiddenAccessException)
            {
                code = HttpStatusCode.Forbidden;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (ex is not UnauthorizedException)
                logger.LogError(ex, "Exception handled in middleware: {@Exception}", ex);

            return context.Response.WriteAsync("error");
        }
    }
}
