//using System;
//using System.Net.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Dispatcher;
//using Castle.Windsor;

//namespace Quilt4.Service.Injection
//{
//    public class WindsorCompositionRoot : IHttpControllerActivator
//    {
//        private readonly IWindsorContainer _container;

//        public WindsorCompositionRoot(IWindsorContainer container)
//        {
//            _container = container;
//        }

//        public IHttpController Create(
//            HttpRequestMessage request,
//            HttpControllerDescriptor controllerDescriptor,
//            Type controllerType)
//        {
//            var controller =
//                (IHttpController) _container.Resolve(controllerType);

//            request.RegisterForDispose(
//                new Release(
//                    () => _container.Release(controller)));

//            return controller;
//        }

//        private sealed class Release : IDisposable
//        {
//            private readonly Action _release;

//            public Release(Action release)
//            {
//                _release = release;
//            }

//            public void Dispose()
//            {
//                _release();
//            }
//        }
//    }
//}