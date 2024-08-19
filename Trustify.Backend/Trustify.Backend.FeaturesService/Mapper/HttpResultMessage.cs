using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesService.Models;
namespace Trustify.Backend.FeaturesService.Mapper
{
    /// <summary>
    /// Resposible for sending result to client based on message from managers
    /// </summary>
    public static class HttpResultMessage
    {
        private const string UnknownError = "There has been an error.";
        private const string NotFoundError = "The record was not found";
        private const string AlreadyExistsError = "The record already exists";
        private const string InvalidDataError = "The data you entered is not valid";
        private const string NotSupported = "The operation is not supported under these conditions";

        /// <summary>
        /// Returns result with appropriate HTTP error code
        /// </summary>
        /// <typeparam name="T">Parameter type that needs to be class</typeparam>
        /// <param name="result">Data to analyze and send to client</param>
        /// <param name="contentType">Defines content type of result</param>
        /// <returns></returns>
        public static ActionResult FilteredResult<T>(ResultMessage<T> result, ContentType contentType = ContentType.Json)
            => result.IsSuccess ? Success(result, contentType) : ErrorWithDetails(result);

        public static ActionResult FilteredResult<ResultMessageTypeWrapper, ResultMessageOriginalType>(ResultMessage<ResultMessageOriginalType> result, IMapper _mapper)
            where ResultMessageTypeWrapper : class
            where ResultMessageOriginalType : class
            => FilteredResult(result.Map<ResultMessageTypeWrapper, ResultMessageOriginalType>(_mapper), ContentType.Json);

        /// <summary>
        /// Returns result with appropriate HTTP error code and detailed message
        /// </summary>
        /// <typeparam name="T">Parameter type that needs to be class</typeparam>
        /// <param name="result">Data to analyze and send to client</param>
        /// <returns></returns>
        public static ActionResult ErrorWithDetails<T>(ResultMessage<T> result)
           => result.Status switch
           {
               OperationStatus.DatabaseError => CreateObjectResult(result, result.Message, (int)HttpStatusCode.InternalServerError),
               OperationStatus.FileSystemError => CreateObjectResult(result, result.Message, (int)HttpStatusCode.InternalServerError),
               OperationStatus.Exists => CreateObjectResult(result, AlreadyExistsError, (int)HttpStatusCode.Conflict),
               OperationStatus.NotFound => CreateObjectResult(result, NotFoundError, (int)HttpStatusCode.UnprocessableEntity),
               OperationStatus.InvalidData => CreateObjectResult(result, InvalidDataError, (int)HttpStatusCode.UnprocessableEntity),
               OperationStatus.UnknownError => CreateObjectResult(result, UnknownError, (int)HttpStatusCode.InternalServerError),
               OperationStatus.NotSupported => CreateObjectResult(result, NotSupported, (int)HttpStatusCode.UnprocessableEntity),
               _ => new BadRequestResult(),

           };

        /// <summary>
        /// Returns result with HTTP success code
        /// </summary>
        /// <typeparam name="T">Parameter type that needs to be class</typeparam>
        /// <param name="result">Data to analyze and send to client</param>
        /// <param name="contentType">Content type of result</param>
        /// <returns></returns>
        public static ActionResult Success<T>(ResultMessage<T> result, ContentType contentType = ContentType.Json)
            => contentType switch
            {
                ContentType.Json => new OkObjectResult(result),
                ContentType.File => new FileContentResult(result.GetFileInfo().data, System.Net.Mime.MediaTypeNames.Application.Octet)
                {
                    FileDownloadName = result.GetFileInfo().name
                },
                _ => new BadRequestResult(),
            };

        private static ObjectResult CreateObjectResult<T>(ResultMessage<T> result, string optionalMessage, int statusCode)
            => new(result.CloneWithDetails(result.Message ?? optionalMessage))
            {
                StatusCode = statusCode
            };
    }
}
