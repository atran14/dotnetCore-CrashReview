using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetCore_CrashReview.Middlewares
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(context);

            stopwatch.Stop();
            await context.Response.WriteAsync($"The request took {stopwatch.ElapsedMilliseconds}ms");
        }   
    }

    public static class StopwatchMiddlewareExtension
    {
        public static IApplicationBuilder UseStopwatch(this IApplicationBuilder app)
        {
            app.UseMiddleware<StopwatchMiddleware>();
            return app;
        }
    }
}