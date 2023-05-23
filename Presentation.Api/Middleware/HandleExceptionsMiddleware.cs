using Newtonsoft.Json;
using Serilog.Context;
using Serilog;
using System.Net;

namespace RWS.Authentication.Api.Middleware
{
    public class HandleExceptionsMiddleware : IMiddleware
    {
        private readonly ILogger<HandleExceptionsMiddleware> logger;

        public HandleExceptionsMiddleware(ILogger<HandleExceptionsMiddleware> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();
                LogContext.PushProperty("ErrorCode", errorId);
                LogContext.PushProperty("StackTrace", exception.StackTrace);
               
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResult = new
                {
                    context.Response.StatusCode,
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception,
                    ErrorId = errorId,
                    SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis.",
                    InnerExceptions = new List<Exception>()
                };
                
                while (exception.InnerException != null)
                {
                    errorResult.InnerExceptions.Add(exception.InnerException);
                    exception = exception.InnerException;
                }                

                logger.LogError(JsonConvert.SerializeObject(errorResult));

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsJsonAsync($"{{ 'Message': {errorResult.SupportMessage} }}");
                }
                else
                {
                    logger.LogWarning("Can't write error response. Response has already started.");
                }
            }
        }
    }
}
