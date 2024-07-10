using Inventory.Api.DTOs;
using System.Net;

namespace Inventory.Api.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;
            var responseDetails = new ExceptionDetailsDto
            {
                CodeResponse = HttpStatusCode.InternalServerError,
                ErrorMessage = "An unexpected error occurred.",
                Errors = new List<string> { exception.Message }
            };
            if (exception is KeyNotFoundException)
            {
                responseDetails.CodeResponse = HttpStatusCode.NotFound;
                responseDetails.ErrorMessage = "Resource not found.";
                responseDetails.Errors = new List<string> { exception.Message };
            }
            else if (exception is ArgumentException)
            {
                responseDetails.CodeResponse = HttpStatusCode.BadRequest;
                responseDetails.ErrorMessage = "Invalid request.";
                responseDetails.Errors = new List<string> { exception.Message };
            }
            _logger.LogError(exception, "An exception occurred.");
            httpContext.Response.StatusCode = (int)responseDetails.CodeResponse;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(responseDetails);
        }
    }
}
