using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext Context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message : {exceptionMessage} , Time Of Occurance",exception.Message,DateTime.UtcNow);
            (string Detail,string Title,int statusCode) details = exception switch
            {
                BadRequestException badRequestException => (badRequestException.Details, "Bad Request", StatusCodes.Status400BadRequest),
                InternalServerException internalServerException => (internalServerException.Details, "Internal Server Error",StatusCodes.Status500InternalServerError),
                NotFoundException notFoundException => (notFoundException.Message, "Not Found",StatusCodes.Status404NotFound),
                ValidationException validation => (validation.Message, "Validation Error",StatusCodes.Status400BadRequest),
                _ => ("An error occurred", "Error",StatusCodes.Status500InternalServerError)
            };


            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.statusCode,
                Detail = details.Detail,
                Instance = Context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", Context.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("errors", validationException.Errors);
            }
            await Context.Response.WriteAsJsonAsync(problemDetails,cancellationToken: cancellationToken);
            return true;
        }
    }
}
