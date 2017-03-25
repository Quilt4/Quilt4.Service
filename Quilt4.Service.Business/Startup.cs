using System.Web.Http;
using Owin;

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

            app.UseWebApi(config);
        }
    }
}