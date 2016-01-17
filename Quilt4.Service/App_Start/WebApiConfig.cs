using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Quilt4.Service.Controllers;
using Quilt4.Service.Injection;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter());

            MapRoutes(config);

            RegisterControllerActivator(container);

            container.Register(Component.For<IConfiguration>().ImplementedBy(typeof(Quilt4Net.Configuration)).LifestyleSingleton());
            container.Register(Component.For<IQuilt4NetClient>().ImplementedBy(typeof(Quilt4Net.Quilt4NetClient)).LifestyleSingleton());
            container.Register(Component.For<ISessionHandler>().ImplementedBy(typeof(Quilt4Net.SessionHandler)).LifestyleSingleton());
            container.Register(Component.For<IIssueHandler>().ImplementedBy(typeof(Quilt4Net.IssueHandler)).LifestyleSingleton());

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Filters.Add(new ExceptionHandlingAttribute(WebApiApplication.LogException));
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute("DefaultApi", "api/{area}/{controller}/{id}", new { id = RouteParameter.Optional, area = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        private static void RegisterControllerActivator(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }

        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API configuration and services
        //    // Configure Web API to use only bearer token authentication.
        //    config.SuppressDefaultHostAuthentication();
        //    config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        //    // Web API routes
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}

        //public static void Register(HttpConfiguration config, IWindsorContainer container)
        //{
        //    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
        //        new CamelCasePropertyNamesContractResolver();

        //    config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter());

        //    MapRoutes(config);

        //    RegisterControllerActivator(container);

        //    var corsAttr = new EnableCorsAttribute("*", "*", "*");
        //    config.EnableCors(corsAttr);
        //}

        //private static void MapRoutes(HttpConfiguration config)
        //{
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
        //        );
        //}
    }
}
