using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spatial.API.Entities;

namespace Calismam1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        readonly string MyAllowOrigins = "_myAllowOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=2416",
                                    x => x.UseNetTopologySuite());
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowOrigins,
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:4200")
                        .WithMethods("GET", "POST", "DELETE", "PUT")
                        .AllowAnyOrigin()
                        .AllowAnyHeader();
                    });
            });
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else {
            //     app.UseExceptionHandler(appError => {
            //         appError.Run(async context => {
            //             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //             context.Response.ContentType = "application/json";

            //             var exception = context.Features.Get<IExceptionHandlerFeature>();
            //             if (exception != null) 
            //             {
            //                 // loglama =>> nlog, elmah
            //                 await context.Response.WriteAsync(new ErrorDetails() {
            //                     StatusCode = context.Response.StatusCode,
            //                     Message = exception.Error.Message
            //                 }.ToString());
            //             }
            //         });
            //     });
            // }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowOrigins);        // Cors hatasını önlemek için !!!!


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
