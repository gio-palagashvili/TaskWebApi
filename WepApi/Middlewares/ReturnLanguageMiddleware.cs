using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace WepApi.Middlewares
{
    public class ReturnLanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public ReturnLanguageMiddleware(RequestDelegate next)
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
                Console.WriteLine(context.Request.Headers.TryGetValue("Language", out _));
            }
        }
    }
}