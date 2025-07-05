using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EnglishVocab.API.Middleware
{
    public static class GlobalResource
    {
        public static string msgRequestNotFound = "Tài nguyên không tồn tại.";
    }

    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                throw new NotFoundException(GlobalResource.msgRequestNotFound);
            }
        }
    }
}
