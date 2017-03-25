using System;
using Microsoft.Owin.Hosting;

namespace Quilt4.Service.Business
{
    public class ServiceHost : IDisposable
    {
        private readonly IDisposable _webApp;

        public ServiceHost()
        {
            _webApp = WebApp.Start<Startup>("http://localhost:8080");
        }

        public void Dispose()
        {
            _webApp?.Dispose();
        }
    }
}