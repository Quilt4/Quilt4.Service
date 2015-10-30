using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
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
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
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

            services.AddTransient<IRepository, MemoryRepository>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IProjectBusiness, ProjectBusiness>();
            services.AddTransient<ISettingBusiness, SettingBusiness>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
