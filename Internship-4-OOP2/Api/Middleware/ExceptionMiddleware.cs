using System.Net;
using Domain.Exceptions;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    message = ex.Message,
                    code = ex.Code,
                    severity = ex.Severity.ToString()
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected server error.");
            }
        }
    }
}
