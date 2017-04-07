using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace Quilt4.Service.Business
{
    public class Service : IDisposable
    {
        private readonly IDisposable _disposable;

        public Service(string url)
        {
            _disposable = WebApp.Start<Startup>(url);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                // Setup the cors middleware to run before SignalR.
                // By default this will allow all origins. You can
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch is already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
        }
    }

    [HubName("MyHub")]
    public class MyHub : Hub
    {
        public string Send(string message)
        {
            Console.WriteLine("Recieved Send function with payload '" + message + "'.");
            Clients.All.addMessage("Echo " + message);
            return "Yeah!";
        }

        //public void DoSomething(string message)
        //{
        //    Console.WriteLine("DoSomething...");
        //    //Clients.All.addMessage("Echo " + message);

        //    //Groups.Add(Context.ConnectionId, groupName);
        //}
    }
}
