using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics; 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TaskWebApi;

namespace WepApi.Middlewares
{
    public class UnhandledMiddleWare : IMiddleware
    {
        public UnhandledMiddleWare()
        {
            
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.StatusCode = 500;
            
            if (context.Features.Get<IExceptionHandlerFeature>() is not null)
            {
                Console.WriteLine(context.Features.Get<IExceptionHandlerFeature>().Error);
            }
            await next(context); 
        }
    }
}