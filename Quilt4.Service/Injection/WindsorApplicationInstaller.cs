//using System.Web.Http;
//using Castle.MicroKernel.Registration;
//using Castle.MicroKernel.SubSystems.Configuration;
//using Castle.Windsor;

//namespace Quilt4.Service.Injection
//{
//    public class WindsorApplicationInstaller : IWindsorInstaller
//    {
//        public void Install(IWindsorContainer container, IConfigurationStore store)
//        {
//            //Repository
//            container.Register(
//                Classes.FromAssemblyNamed("Quilt4.Service.Repository")
//                    .InNamespace("Quilt4.Service.Repository.SqlRepository")
//                    .WithService.DefaultInterfaces()
//                    .LifestyleSingleton());

//            //Business
//            container.Register(
//                Classes.FromAssemblyNamed("Quilt4.Service.Business")
//                    .InNamespace("Quilt4.Service.Business")
//                    .WithService.DefaultInterfaces()
//                    .LifestyleTransient());

//            //Register controllers
//            container.Register(Classes.FromThisAssembly()
//                .BasedOn<ApiController>()
//                .LifestylePerWebRequest());
//        }
//    }
//}