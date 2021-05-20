using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskWeb.Repository;
using TaskWebApi;

namespace WepApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                var request = new RequestLog {Method = context.Request.Method, Path = context.Request.Path.Value, StatusCode = context.Response.StatusCode.ToString()};
                await RequestLogger.Log(request);
            }
        }
    }
}