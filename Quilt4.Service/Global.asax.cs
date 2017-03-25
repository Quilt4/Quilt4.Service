using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Quilt4.Service.Business;
using Quilt4.Service.Business.Command;
using Quilt4.Service.Entity;
using Quilt4.Service.Injection;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Quilt4Net.Core;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service
{
    public class WebApiApplication : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureWindsor(GlobalConfiguration.Configuration);

            RegisterServiceLogger();

            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageHandler(_container.Resolve<ICommandQueue>())); //_container.Resolve<IServiceBusiness>(), _container.Resolve<IServiceLog>()));

            RegisterSession();
        }

        protected void Application_BeginRequest()
        {
            //CanWriteToSystemLog();
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

            //_container.Install(new WindsorApplicationInstaller());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;

            var castleControllerFactory = new CastleControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);
        }

        protected void Application_End()
        {
            _container.Dispose();
            Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = null;
            try
            {
                lastError = Server.GetLastError();
                LogException(lastError, LogLevel.SystemError);
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("<html><body>");
                HttpContext.Current.Response.Write("Error in the error handler. " + exception.Message + "</br>");
                if (lastError != null)
                {
                    HttpContext.Current.Response.Write("Original error. " + lastError.Message + "</br>");
                }

                HttpContext.Current.Response.End();
            }
        }

        private void RegisterSession()
        {
            var sessionHandler = _container.Resolve<Quilt4Net.Interfaces.ISessionHandler>();
            sessionHandler.SetFirstAssembly(Assembly.GetAssembly(typeof(WebApiApplication)));

            if (!HasOwnProjectApiKey())
                return;

            sessionHandler.SessionRegistrationCompletedEvent += SessionHandler_SessionRegistrationCompletedEvent;
            sessionHandler.RegisterStart();
        }

        private void SessionHandler_SessionRegistrationCompletedEvent(object sender, Quilt4Net.Core.Events.SessionRegistrationCompletedEventArgs e)
        {
            if (!e.Result.IsSuccess)
            {
                LogException(e.Result.Exception, LogLevel.SystemError);
            }
        }

        public static Guid? LogException(Exception exception, LogLevel logLevel)
        {
            if (exception == null) return null;

            try
            {
                ExceptionIssueLevel? issueLevel;
                switch (logLevel)
                {
                    case LogLevel.DoNotLog:
                        return null;
                    case LogLevel.Error:
                        issueLevel = ExceptionIssueLevel.Error;
                        break;
                    case LogLevel.Warning:
                        issueLevel = ExceptionIssueLevel.Warning;
                        break;
                    case LogLevel.Information:
                        issueLevel = ExceptionIssueLevel.Information;
                        break;
                    case LogLevel.SystemError:
                        issueLevel = null;
                        break;
                    default:
                        issueLevel = null;
                        break;
                }

                if (!HasOwnProjectApiKey())
                {
                    issueLevel = null;
                }

                if (issueLevel != null)
                {
                    var issueHandler = _container.Resolve<IIssueHandler>();
                    issueHandler.IssueRegistrationCompletedEvent += IssueHandler_IssueRegistrationCompletedEvent;
                    var response = issueHandler.Register(exception, issueLevel.Value);
                    return response.Response.IssueKey;
                }
                else
                {
                    var log = _container.Resolve<IServiceLog>();
                    log.LogException(exception, logLevel);
                    return Guid.Empty;
                }
            }
            catch (Exception exp)
            {
                HttpContext.Current.Response.Write("<html><body>");
                HttpContext.Current.Response.Write("Unable to log exception. Reason: " + exp.Message + "</br>");
                HttpContext.Current.Response.Write("The original exception that could not be logged: " + exception.Message + "</br>");
                HttpContext.Current.Response.Write("</body></html>");
                HttpContext.Current.Response.End();
                return Guid.Empty;
            }
        }

        private static bool HasOwnProjectApiKey()
        {
            try
            {
                var settingBusiness = _container.Resolve<ISettingBusiness>();
                var hasOwnProjectApiKey = settingBusiness.HasSetting(ConstantSettingKey.ProjectApiKey, true);
                return hasOwnProjectApiKey;
            }
            catch (SqlException)
            {
                //TODO: This should be visible on the site
                return false;
            }
        }

        private static void IssueHandler_IssueRegistrationCompletedEvent(object sender, Quilt4Net.Core.Events.IssueRegistrationCompletedEventArgs e)
        {
            if (!e.Result.IsSuccess)
            {
                LogException(e.Result.Exception, LogLevel.SystemError);
            }
        }

        internal static ISourceRepository GetSourceRepository()
        {
            var repo = _container.Resolve<ISourceRepository>();
            return repo;
        }

        internal static IUserRepository GetUserRepository()
        {
            var repo = _container.Resolve<IUserRepository>();
            return repo;
        }
    }
}