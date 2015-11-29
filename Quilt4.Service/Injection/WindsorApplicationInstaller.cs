﻿using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Quilt4.Service.Injection
{
    public class WindsorApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Data Repository
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.SqlRepository")
                    .InNamespace("Quilt4.Service.SqlRepository.Data")
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton());

            //Read Repository
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.SqlRepository")
                    .InNamespace("Quilt4.Service.SqlRepository.Read")
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton());

            //Business
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.Business")
                    .InNamespace("Quilt4.Service.Business")
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

            //Command handlers
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.Business")
                    .InNamespace("Quilt4.Service.Business.Handlers.Commands")
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

            //Query handlers
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.Business")
                    .InNamespace("Quilt4.Service.Business.Handlers.Queries")
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

            //Register controllers
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest());
        }
    }
}