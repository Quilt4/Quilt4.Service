using System.Collections.Concurrent;
using System.Threading;

namespace Quilt4.Service.Business.Command
{
    public class CommandQueue : ICommandQueue
    {
        private readonly BlockingCollection<ICommand> _queue = new BlockingCollection<ICommand>();
        private readonly AutoResetEvent _itemAdded = new AutoResetEvent(false);

        public void Enqueue(ICommand command)
        {
            //TODO: Persist command to repository here

            _queue.Add(command);
            _itemAdded.Set();
        }

        public bool TryTake(out ICommand command)
        {
            var result = _queue.TryTake(out command);
            return result;
        }

        public AutoResetEvent ItemAdded => _itemAdded;
    }
}