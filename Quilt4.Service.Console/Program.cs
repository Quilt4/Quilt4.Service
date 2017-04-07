using Quilt4.Service.Console.SourceCommands;
using Tharga.Toolkit.Console.Command;
using Tharga.Toolkit.Console.Command.Base;

namespace Quilt4.Service.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (new Business.Service("http://localhost:8088"))
            {
                var rootCommand = new RootCommand(new ClientConsole());
                rootCommand.RegisterCommand(new SourceCommand());
                var engine = new Tharga.Toolkit.Console.CommandEngine(rootCommand);
                engine.Run(args);
            }
        }
    }
}