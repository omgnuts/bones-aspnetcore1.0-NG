﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using api.Models;
using api.Resources;


namespace api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            {
                // Enable CORS
                services.AddCors(options => buildCorsOptions(options));
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
                });
            }

            services.AddMvc()
                // return JSON response in form of Camel Case so that we can sure consume the API in any client. 
                // Enable CamelCasePropertyNamesContractResolver in Configure Services.
                .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // using Dependency Injection
            services.AddSingleton<IResource<Todo>, TodoResource>();
            services.AddSingleton<IResource<Contact>, ContactResource>();
            services.AddSingleton<IResource<Student>, StudentResource>();
            services.AddSingleton<IResource<Worker>, WorkerResource>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");
            
            // Perform the routing
            app.UseMvc();
        }

        private static void buildCorsOptions(CorsOptions options)
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            //options.AddPolicy("AllowSpecificOrigin",
            //    builder =>
            //    {
            //        builder.WithOrigins("http://localhost:18080")
            //               .WithMethods("GET", "POST")
            //               .AllowAnyHeader();
            //    });
        }
    }

}
