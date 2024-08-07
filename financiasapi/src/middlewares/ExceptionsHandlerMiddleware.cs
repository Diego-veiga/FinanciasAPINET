using System.Text.Json;
using financiasapi.src.models;
using Microsoft.IdentityModel.Tokens;

namespace financias.src.middlewares
{
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<ExceptionsHandlerMiddleware> _logger;

        public ExceptionsHandlerMiddleware(RequestDelegate request, ILogger<ExceptionsHandlerMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await _request(httpContent);
                if (httpContent.Response.StatusCode == 401)
                {
                    httpContent.Response.ContentType = "application/json";
                    var errorResponse = new ResponseHandlerError()
                    {
                        Error = "Token Invalid",
                    };
                    var result = JsonSerializer.Serialize(errorResponse);
                    await httpContent.Response.WriteAsync(result);
                }

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContent, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ResponseHandlerError();

            switch (exception)
            {
                case ApplicationException ex:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    errorResponse.Error = ex.Message;
                    break;
                case SecurityTokenException ex:
                    response.StatusCode = StatusCodes.Status403Forbidden;
                    errorResponse.Error = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    errorResponse.Error = ex.Message;
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    errorResponse.Error = "Internal Server error.";
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }

    }

}
