using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetCore_CrashReview.Middlewares
{
    public class QueryStringToHeaderMigrationMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryStringToHeaderMigrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // await context.Response.WriteAsync("The query string to header migration middleware has been called");

            foreach(var query in context.Request.Query)
            {
                context.Request.Headers.Add(query.Key, query.Value);
            }

            foreach(var tuple in context.Request.Headers)
            {
                await context.Response.WriteAsync($"{tuple.Key} = {tuple.Value}\n");
            }
            await _next(context);
        }
    }

    public static class QueryStringToHeaderMigrationMiddlewareExtension
    {
        public static IApplicationBuilder UseQueryStringToHeaderMigrationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<QueryStringToHeaderMigrationMiddleware>();
            return app;
        }
    }
}