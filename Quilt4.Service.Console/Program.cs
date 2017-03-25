using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quilt4.Service.Console.SourceCommands;
using Tharga.Toolkit.Console.Command;
using Tharga.Toolkit.Console.Command.Base;

namespace Quilt4.Service.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new RootCommand(new ClientConsole());
            rootCommand.RegisterCommand(new SourceCommand());
            var engine = new Tharga.Toolkit.Console.CommandEngine(rootCommand);
            engine.Run(args);
        }
    }
}
