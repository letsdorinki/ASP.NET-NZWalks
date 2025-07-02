using System.Net;
using System.Net.WebSockets;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
            
                {
                var errorId = Guid.NewGuid();
                //log this Exception

                logger.LogError(ex, $"{errorId}:{ex.Message}");

                //return a custom Error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage="Something went wrong !we are looking into resolving this."
                };
                await httpContext.Response.WriteAsJsonAsync(error);

                }
        }
    }
}
