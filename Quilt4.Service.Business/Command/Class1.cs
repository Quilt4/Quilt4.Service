using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quilt4.Service.Business.Command
{
    public class CommandRunner
    {
        private readonly Task _task;

        public CommandRunner(ICommandQueue commandQueue)
        {
            _task = new Task(() =>
            {
                while (true)
                {
                    commandQueue.ItemAdded.WaitOne();

                    object item;
                    while (commandQueue.TryTake(out item))
                    {
                        //TODO: Handle the command
                        //TODO: Fire event, returning the result, using signalR.
                    }
                }

                System.Diagnostics.Debug.WriteLine("Exiting command loop.");
            });
        }

        public void Run()
        {
            _task.Start();
        }
    }

    public class CommandQueue : ICommandQueue
    {
        //private readonly BlockingCollection<HttpRequestMessage> _queue = new BlockingCollection<HttpRequestMessage>();
        private readonly AutoResetEvent _itemAdded = new AutoResetEvent(false);

        public void Enqueue(object command) //HttpRequestMessage request)
        {
            //TODO: Store command in database (So that it can be replayed later)
            //_queue.Add(request);
            //_itemAdded.Set();
        }

        public bool TryTake(out object command)
        {
            //return _queue.TryTake(out item);
            throw new NotImplementedException();
        }

        public AutoResetEvent ItemAdded => _itemAdded;
    }

    public interface ICommandQueue
    {
        void Enqueue(object command); //HttpRequestMessage request);
        bool TryTake(out object command);
        AutoResetEvent ItemAdded { get; }
    }
}