using IPInfo.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IPInfo.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var exceptionDetails = new ExceptionDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Unexpected Error"
            };

            HandleException(ex, exceptionDetails);

            var result = JsonConvert.SerializeObject(new { error = exceptionDetails.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exceptionDetails.StatusCode ?? 500;

            return context.Response.WriteAsync(result);
        }

        private void HandleException(Exception ex, ExceptionDetails exceptionDetails)
        {
            _logger.Error(ex, ex.Message);

            if (ex is BadRequestException)
            {
                exceptionDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                exceptionDetails.Message = ex.Message;
            }
            else if (ex is NotFoundException)
            {
                exceptionDetails.StatusCode = (int)HttpStatusCode.NotFound;
                exceptionDetails.Message = ex.Message;
            }
            else if (ex is IPInfoException)
            {
                exceptionDetails.StatusCode = (ex as IPInfoException).StatusCode;
                exceptionDetails.Message = ex.Message;
            }
        }
    }
}
