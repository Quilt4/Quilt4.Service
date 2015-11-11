using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Quilt4.Service.Injection;

namespace Quilt4.Service
{
    public class WebApiApplication : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());

            _container.Install(new WindsorApplicationInstaller());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;
        }

        protected void Application_End()
        {
            _container.Dispose();
            base.Dispose();
        }
    }
}