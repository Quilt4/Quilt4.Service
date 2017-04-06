using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Quilt4.Service.Business.Command;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Quilt4.Service.Business
{
    public interface IRequestMessageAccessor
    {
        HttpRequestMessage CurrentMessage { get; }
    }

    internal sealed class RequestMessageAccessor : IRequestMessageAccessor
    {
        private readonly Container container;

        public RequestMessageAccessor(Container container)
        {
            this.container = container;
        }

        public HttpRequestMessage CurrentMessage
        {
            get { return this.container.GetCurrentHttpRequestMessage(); }
        }
    }

    public class ServiceHost : IDisposable
    {
        private readonly IDisposable _webApp;
        private readonly CommandRunner _commandRunner;

        internal static Container _container;

        public ServiceHost()
        {
            var commandQueue = new CommandQueue();
            _commandRunner = new CommandRunner(commandQueue);
            _commandRunner.Run();

            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            _container.Register<ICommandQueue, CommandQueue>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            _container.RegisterWebApiControllers(GlobalConfiguration.Configuration, Assembly.GetExecutingAssembly());
            _container.Verify();

            //container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);
            //container.RegisterSingleton<IRequestMessageAccessor>(new RequestMessageAccessor(container));

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);



            _webApp = WebApp.Start<Startup>("http://localhost:8080");
        }

        //private static void NewMethod()
        //{
        //    var container = new Container();
        //    container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

        //    // Register your types, for instance using the scoped lifestyle:
        //    //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        //    container.Register<ICommandQueue, CommandQueue>(Lifestyle.Singleton);

        //    // This is an extension method from the integration package.
        //    //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
        //    container.RegisterWebApiControllers(GlobalConfiguration.Configuration, Assembly.GetExecutingAssembly());
        //    container.Verify();

        //    //container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);
        //    //container.RegisterSingleton<IRequestMessageAccessor>(new RequestMessageAccessor(container));

        //    GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        //}

        //public void Start()
        //{
        //    var container = new Container();
        //    container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

        //    // Register your types, for instance using the scoped lifestyle:
        //    //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        //    container.Register<ICommandQueue, CommandQueue>(Lifestyle.Scoped);

        //    // This is an extension method from the integration package.
        //    //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
        //    container.RegisterWebApiControllers(
        //        GlobalConfiguration.Configuration,
        //        Assembly.GetExecutingAssembly()
        //    );
        //    container.Verify();

        //    GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        //}

        public void Dispose()
        {
            _commandRunner.Stop();
            _webApp?.Dispose();
        }
    }
}