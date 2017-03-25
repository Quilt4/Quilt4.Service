using System.Threading.Tasks;
using Tharga.Toolkit.Console.Command.Base;

namespace Quilt4.Service.Console.SourceCommands
{
    internal class SourceCommand : ContainerCommandBase
    {
        public SourceCommand()
            : base("Source")
        {
            RegisterCommand(new SourceList());
            RegisterCommand(new SourceClear());
            RegisterCommand(new SourceReplay());
        }
    }

    internal class SourceList : ActionCommandBase
    {
        public SourceList()
            : base("List", "List all source commands.")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            return true;
        }
    }

    internal class SourceClear : ActionCommandBase
    {
        public SourceClear()
            : base("Clear", "Clear the read store.")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            return true;
        }
    }

    internal class SourceReplay : ActionCommandBase
    {
        public SourceReplay()
            : base("Replay", "Replay the source commands.")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            return true;
        }
    }
}
