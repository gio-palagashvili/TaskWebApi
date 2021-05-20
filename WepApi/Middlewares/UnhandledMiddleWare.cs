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
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }
    }
}