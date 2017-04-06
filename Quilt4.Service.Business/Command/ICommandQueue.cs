using System;
using System.Threading;

namespace Quilt4.Service.Business.Command
{
    public abstract class CommandHandlerBase<T>
    {
        public abstract void Execute(T command);
    }

    public class SomeCommandHandler : CommandHandlerBase<SomeCommand>
    {
        public override void Execute(SomeCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public class SomeCommand : ICommand
    {
        public string SomeData { get; set; }
    }

    public interface ICommand
    {
    }

    public interface ICommandQueue
    {
        void Enqueue(ICommand command);
        bool TryTake(out ICommand command);
        AutoResetEvent ItemAdded { get; }
    }
}