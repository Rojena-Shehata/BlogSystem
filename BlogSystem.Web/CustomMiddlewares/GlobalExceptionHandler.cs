using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Web.CustomMiddlewares
{
    public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService,ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError("Unhandled Exception Occurred");

            int statusCode = exception switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError

            };
            httpContext.Response.StatusCode = statusCode;
          return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext{
                    HttpContext = httpContext,
                    ProblemDetails=new ProblemDetails()
                    {
                        Title="An Error Occurred",
                        Detail=exception.Message,
                        Type=exception.GetType().Name,
                        Status=statusCode,
                        Instance=httpContext.Request.Path
                    }
                    
           });
        }
    }
}
