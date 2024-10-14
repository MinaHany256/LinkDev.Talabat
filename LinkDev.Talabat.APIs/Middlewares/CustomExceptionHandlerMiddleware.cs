using LinkDev.Talabat.APIs.Controllers.Errors;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Middlewares
{

    // Convension-Based
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Logic Executed with the request

                await _next(httpContext);

               

                // Logic Executed with the response

            }
            catch (Exception ex)
            {
                ApiExceptionResponse response;

                if (_env.IsDevelopment())
                {
                    // Development Mode
                    _logger.LogError(ex, ex.Message);
                    response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());
                }
                else
                {
                    // Production Mode
                    response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                }

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));

            }
        }

    }
}
