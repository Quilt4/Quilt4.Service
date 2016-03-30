using System.Web.Http;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service.Injection
{
    public class WindsorApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Quilt4Net
            container.Register(Component.For<IConfiguration>().ImplementedBy(typeof(Quilt4Net.Configuration)).LifestyleSingleton());
            container.Register(Component.For<IQuilt4NetClient>().ImplementedBy(typeof(Quilt4Net.Quilt4NetClient)).LifestyleSingleton());
            container.Register(Component.For<Quilt4Net.Interfaces.ISessionHandler>().ImplementedBy(typeof(Quilt4Net.SessionHandler)).LifestyleSingleton());
            container.Register(Component.For<IIssueHandler>().ImplementedBy(typeof(Quilt4Net.IssueHandler)).LifestyleSingleton());

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

            //Register ApiController
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest());

            //Register controllers
            container.Register(Classes.FromThisAssembly()
                .BasedOn<Controller>()
                .LifestylePerWebRequest());
        }
    }
}