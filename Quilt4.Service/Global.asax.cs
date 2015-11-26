using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Quilt4.Service.Business;
using Quilt4.Service.Injection;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
{
    public class WebApiApplication : HttpApplication
    {
        private static IWindsorContainer _container;
        private WriteBusiness _writeBusiness;

        protected void Application_Start()
        {
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));
            _writeBusiness = new WriteBusiness(_container.Kernel.Resolve<IWriteRepository>());
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
            _writeBusiness.Dispose();
            _container.Dispose();
            Dispose();
        }
    }
}