using System;
using System.Collections.Generic;

namespace Medli.Apps;

/// <summary>
/// The 'help' command. Ported from Medli/Kernel/Utils/Shell/Help.cs.
/// The paged ("help pause") variant is dropped until KernelExtensions is ported.
/// </summary>
public class HelpCommand : Command
{
    private readonly List<Command> _commands;

    public HelpCommand(List<Command> commands) => _commands = commands;

    public override string Name => "help";
    public override string Summary => "Gets help on a specific command.";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
            DisplayCommands();
        else
            CommandHelp(param);
    }

    private void CommandHelp(string command)
    {
        foreach (var c in _commands)
        {
            if (c.Name == command)
            {
                c.Help();
                Console.WriteLine();
                return;
            }
        }
        NotSupported(command);
    }

    private void DisplayCommands()
    {
        Console.WriteLine("Supported Commands:");
        foreach (var c in _commands)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  " + c.Name + ":  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(c.Summary);
        }
        Console.WriteLine("Please type help [command] for more information.");
        Console.WriteLine();
    }

    private static void NotSupported(string command)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The command " + command + " is not supported. Please type help for more information.");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    public override void Help()
    {
        Console.WriteLine("help [command]");
        Console.WriteLine("  Gets help on a specific command.");
        Console.WriteLine("  [command]: The command to look up.");
    }
}
