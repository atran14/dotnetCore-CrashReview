using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dotnetCore_CrashReview.Middlewares;

namespace dotnetCore_CrashReview
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseStopwatch();

            app.UseQueryStringToHeaderMigrationMiddleware();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1 - Going in\n");                
                await next();
                await context.Response.WriteAsync("Middleware 1 - Going out\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2 - Going in\n");                
                await next();
                await context.Response.WriteAsync("Middleware 2 - Going out\n");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Middleware 3 - Terminal\n");
                await context.Response.WriteAsync("Hello, World! 1*\n");
                await context.Response.WriteAsync("Middleware 3 - Done execution!\n");
            }
            );

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Error");
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapRazorPages();
            // });
        }
    }
}
