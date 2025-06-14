using RestCountries.Application.Common.Exceptions;

namespace RestCountries.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (AppException ex)
            {
                _logger.LogError(ex, "An exception occured");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occured");
                await HandleExceptionAsync(context, new InternalServerErrorException("Internal Server Error"));
            }
        }

        private Task HandleExceptionAsync(HttpContext context, AppException ex)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.Message,
                status = ex.StatusCode
            };
            context.Response.StatusCode = ex.StatusCode;
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
