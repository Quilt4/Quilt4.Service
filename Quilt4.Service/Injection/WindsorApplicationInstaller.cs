﻿using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Injection
{
    public class WindsorApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Repository
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.SqlRepository")
                    .InNamespace("Quilt4.Service.SqlRepository")
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton());

            //Business
            container.Register(
                Classes.FromAssemblyNamed("Quilt4.Service.Business")
                    .InNamespace("Quilt4.Service.Business")
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

            //Register controllers
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest());
        }
    }
}