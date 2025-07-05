using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EnglishVocab.API.Middleware
{
    // Định nghĩa các lớp ngoại lệ
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; set; }

        public BadRequestException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public BadRequestException(string message, IDictionary<string, string[]> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    // Định nghĩa các lớp kết quả
    public abstract class FuncResult<T>
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }

    public class FailResult<T> : FuncResult<T>
    {
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            FuncResult<bool> responseResult = null;

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;

                    responseResult = new FailResult<bool>()
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    responseResult = new FailResult<bool>()
                    {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Detail = notFoundException.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;

                default:
                    responseResult = new FailResult<bool>()
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.InnerException?.Message,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            _logger.LogError("ExceptionMiddleware response error", new Exception(responseResult?.Title));

            await httpContext.Response.WriteAsJsonAsync(responseResult);
        }
    }
}
