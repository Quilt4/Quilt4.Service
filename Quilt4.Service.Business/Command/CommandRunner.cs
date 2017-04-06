using System.Threading.Tasks;

namespace Quilt4.Service.Business.Command
{
    public class CommandRunner
    {
        private readonly Task _task;
        private readonly ICommandQueue _commandQueue;
        private bool _running = true;

        public CommandRunner(ICommandQueue commandQueue)
        {
            _commandQueue = commandQueue;

            _task = new Task(() =>
            {
                while (_running)
                {
                    commandQueue.ItemAdded.WaitOne();

                    ICommand item;
                    while (commandQueue.TryTake(out item))
                    {
                        //Create a command handler of type 
                        //SimpleInjector.reg

                        //TODO: Execute the command
                        //TODO: Fire event, returning the result, using signalR.
                    }
                }
            });
        }

        public void Run()
        {
            _task.Start();
        }

        public void Stop()
        {
            _running = false;
            _commandQueue.ItemAdded.Set();
        }
    }
}