using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Quilt4.Service.Injection;

namespace Quilt4.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            MapRoutes(config);

            RegisterControllerActivator(container);
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }

        private static void RegisterControllerActivator(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerActivator),
                new WindsorCompositionRoot(container));
        }
    }
}