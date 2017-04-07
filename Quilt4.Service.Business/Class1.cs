using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using Quilt4Net.Core.DataTransfer;

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

                Console.WriteLine("mapped");
            });

            Console.WriteLine("configured");
        }
    }

    //public interface ICommand
    //{
    //}

    //public abstract class BaseCommandHandler<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    //{
    //    public ISessionFactory SessionFactory { get; set; }
    //    public IConnectionManager ConnectionManager { get; set; }

    //    protected ISession Session
    //    {
    //        get { return SessionFactory.GetCurrentSession(); }
    //    }

    //    #region Implementation of IHandleCommand<TCommand>
    //    public abstract void Handle(TCommand command);
    //    #endregion
    //}

    public class CommandDto
    {
        public Guid CommandKey { get; set; }
        public string Name { get; set; }
        public object Data { get; set; }
    }

    [HubName("MyHub")]
    public class MyHub : Hub
    {
        private readonly BlockingCollection<CommandDto> _commands = new BlockingCollection<CommandDto>();

        public MyHub()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var item = _commands.Take();
                    var type = Type.GetType($"Quilt4.Service.Business.{item.Name}Handler, Quilt4.Service.Business");
                    if(type == null) throw new InvalidOperationException($"Cannot find {item.Name}Handler class. Unable to handle command");
                    var obj = Activator.CreateInstance(type);
                    var m = obj.GetType().GetMethod("Execute");

                    var data = new object();
                    //var dataType = Type.GetType(item.Name);
                    //var payload = Activator.CreateInstance(dataType);

                    m.Invoke(obj, new[] { data });
                }
            });
        }

        public override Task OnConnected()
        {
            Console.WriteLine("OnConnected");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("OnDisconnected");
            return base.OnDisconnected(stopCalled);
        }

        public Guid Execute(CommandDto command)
        {
            //TODO: Persist this command in a event source database. So that it can be replayed later
            _commands.Add(command);
            return command.CommandKey;
        }
    }

    public abstract class HandlerBase<T>
    {
        public abstract void Execute(T command);
    }

    public class IssueRequestHandler : HandlerBase<IssueRequest>
    {
        public override void Execute(IssueRequest command)
        {
        }
    }

    public class SessionRequestHandler : HandlerBase<SessionRequest>
    {
        public override void Execute(SessionRequest command)
        {
        }
    }
}