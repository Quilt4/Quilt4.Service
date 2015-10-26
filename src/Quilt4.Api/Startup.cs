using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Quilt4.Api.Business;
using Quilt4.Api.Interfaces;
using Quilt4.Api.Repositories;

namespace Quilt4.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
            }); 

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin());
            });
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            services.AddTransient<IRepository, MemoryRepository>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IProjectBusiness, ProjectBusiness>();
            services.AddTransient<ISettingBusiness, SettingBusiness>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Adding delay to every call
            //For testing loading-panel on the web
            app.Use(next => async context =>
            {
                var rand = new Random(1).Next(5);
                Console.WriteLine(rand);
                Thread.Sleep(1000 * rand);

                await next.Invoke(context);
                
            });


            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            
        }
    }
}
