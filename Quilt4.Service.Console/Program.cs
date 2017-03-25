using Quilt4.Service.Console.SourceCommands;
using Tharga.Toolkit.Console.Command;
using Tharga.Toolkit.Console.Command.Base;
using Quilt4.Service.Business;
using Quilt4.Service.Business.Command;

namespace Quilt4.Service.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            //TODO: Set up a signal-R back to the caller

            var commandQueue = new CommandQueue();
            var cr = new CommandRunner(commandQueue);
            cr.Run();

            using (new ServiceHost())
            {
                var rootCommand = new RootCommand(new ClientConsole());
                rootCommand.RegisterCommand(new SourceCommand());
                var engine = new Tharga.Toolkit.Console.CommandEngine(rootCommand);
                engine.Run(args);
            }
        }
    }
}
