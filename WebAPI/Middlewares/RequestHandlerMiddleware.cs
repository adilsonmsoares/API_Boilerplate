using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Middlewares
{
    public class RequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<RequestHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, logger, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger, (int)HttpStatusCode.InternalServerError);
            }
        }

        #region Private Methods

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger, int statusCode)
        {
            var message = CreateErrorMessage(exception, statusCode);

            LogError(logger, exception);

            await WriteResponseAsync(context, message, statusCode);
        }

        private async Task WriteResponseAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(message);
        }

        private static void LogError(ILogger logger, Exception exception)
        {
            string methodName = $"{new StackFrame(1, false).GetMethod().DeclaringType.Name}.{new StackFrame(1).GetMethod().Name}";
            logger.LogError($"MethodName: {methodName} Exception: {exception}");
        }

        private string CreateErrorMessage(Exception exception, int statusCode)
        {
            var errors = new List<ErrorResponseModel>();

            if (statusCode == (int)HttpStatusCode.BadRequest)
            {
                errors.AddRange(JsonSerializer
                    .Deserialize<List<string>>(exception.Message)
                    .Select(e => new ErrorResponseModel() { Code = statusCode.ToString(), Message = e }));
            }
            else
            {
                errors.Add(new ErrorResponseModel() { Code = statusCode.ToString(), Message = exception.Message });
            }

            return JsonSerializer.Serialize(new DefaultResponseModel()
            {
                Success = false,
                Errors = errors
            });
        }
        #endregion
    }
}
