using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System;
using Serilog.Context;
using System.Security.Claims;

namespace Trustify.Backend.ImageGeneratorService.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            HttpRequest? request = httpContext.Request;

            DateTime requestTime = DateTime.UtcNow;

            try
            {
                if (request.Headers.ContainsKey("async-api")
                    || request.ContentType == System.Net.Mime.MediaTypeNames.Application.Octet
                    || request.Path.Value?.EndsWith("client-machine") == true)
                {
                    await next(httpContext);
                    return;
                }

                var stopWatch = Stopwatch.StartNew();
                string requestBodyContent = await ReadRequestBody(request);

                Stream? originalBodyStream = httpContext.Response.Body;
                using var responseBody = new MemoryStream();
                HttpResponse? response = httpContext.Response;
                response.Body = responseBody;

                if (requestBodyContent.Length > 200)
                    requestBodyContent = requestBodyContent[0..200];
                logger.LogInformation("Http request {@Metadata}", requestBodyContent);

                await next(httpContext);

                stopWatch.Stop();

                string responseBodyContent = string.Empty;
                responseBodyContent = await ReadResponseBody(response);
                await responseBody.CopyToAsync(originalBodyStream);

                if (responseBodyContent.Length > 200)
                    responseBodyContent = responseBodyContent[0..200];
                logger.LogInformation("Http response {@Metadata}", new { Body = responseBodyContent, response.StatusCode, stopWatch.ElapsedMilliseconds });
            }
            catch
            {
                throw;
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            if (request == null)
                return string.Empty;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength, CultureInfo.InvariantCulture)];
            await request.Body.ReadAsync(buffer);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response?.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response?.Body ?? Stream.Null).ReadToEndAsync();
            response?.Body?.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}
