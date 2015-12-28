using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Quilt4.Service.Injection;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));

            RegisterServiceLogger();

            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        protected void Application_BeginRequest()
        {
            CanWriteToLog();
        }

        private static void RegisterServiceLogger()
        {
            var serviceLog = System.Configuration.ConfigurationManager.AppSettings["ServiceLog"];
            if (!string.IsNullOrEmpty(serviceLog))
            {
                _container.Register(Component.For<IServiceLog>().ImplementedBy(Type.GetType(serviceLog)).LifestyleSingleton());
            }
        }

        private static void ConfigureWindsor(HttpConfiguration configuration)
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
            Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            LogException(lastError);
        }

        private void CanWriteToLog()
        {
            var log = _container.Resolve<IServiceLog>();
            Exception exception;
            if (!log.CanWriteToLog(out exception))
            {
                var serviceLog = System.Configuration.ConfigurationManager.AppSettings["ServiceLog"];
                HttpContext.Current.Response.Write("Unable to write to service log (" + serviceLog + ").<br/>");
                HttpContext.Current.Response.Write("Exception: " + exception.Message + "<br/><br/>");
                HttpContext.Current.Response.Write("If the event log is used, the service might not have access to create the event source. Run the service as administrator once to create the source, then the service can run as a regular user.<br/>");
                HttpContext.Current.Response.End();
            }
        }

        public static void LogException(Exception exception)
        {
            try
            {
                var log = _container.Resolve<IServiceLog>();
                log.LogException(exception);
            }
            catch (Exception exp)
            {
                HttpContext.Current.Response.Write("Unable to log exception. Reason: " + exp.Message + "</br>");
                HttpContext.Current.Response.Write("The original exception that could not be logged: " + exception.Message + "</br>");
                HttpContext.Current.Response.End();
            }
        }

        internal static IRepository GetRepository()
        {
            var repo = _container.Resolve<IRepository>();
            return repo;
        }
    }
}