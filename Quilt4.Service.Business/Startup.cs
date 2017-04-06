using System.Web.Http;
using Owin;
using Quilt4.Service.Business.Command;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Quilt4.Service.Business
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{handler}/{data}",
                defaults: new { data = RouteParameter.Optional }
            );

            //GlobalConfiguration.Configuration.DependencyResolver.getco

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(ServiceHost._container);

            app.UseWebApi(config);
        }
    }
}