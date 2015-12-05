//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http.Dependencies;
//using Castle.Windsor;

//namespace Quilt4.Service.Injection
//{
//    public class WindsorDependencyResolver : IDependencyResolver
//    {
//        private readonly IWindsorContainer _container;

//        public WindsorDependencyResolver(IWindsorContainer container)
//        {
//            _container = container;
//        }

//        public IDependencyScope BeginScope()
//        {
//            return new WindsorDependencyScope(_container);
//        }

//        public object GetService(Type serviceType)
//        {
//            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            if (!_container.Kernel.HasComponent(serviceType))
//            {
//                return new object[0];
//            }

//            return _container.ResolveAll(serviceType).Cast<object>();
//        }

//        public void Dispose()
//        {
//            _container.Dispose();
//        }
//    }
//}