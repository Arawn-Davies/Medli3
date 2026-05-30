using System;
using System.Collections.Generic;
using Medli.Apps;

namespace Medli;

/// <summary>
/// The interactive shell. Slimmed port of Medli/Kernel/CommandConsole.cs: keeps the
/// command registry + parser, but drops the gen2 login/screen/filesystem state and
/// only registers commands that have been ported so far (no IO yet).
/// </summary>
public class CommandConsole
{
    private readonly List<Command> _commands = new List<Command>();
    private bool _running = true;

    public CommandConsole()
    {
        _commands.Add(new Clear());
        _commands.Add(new Echo());
        _commands.Add(new Set());
        _commands.Add(new Apps.Date());
        _commands.Add(new Apps.Time());
        _commands.Add(new Apps.Version());
        _commands.Add(new Reboot());
        _commands.Add(new Shutdown(Stop));
        _commands.Add(new Panic());
        // Help is last so it can list every other command.
        _commands.Add(new HelpCommand(_commands));
    }

    /// <summary>Stops the shell loop (used by the 'exit' command).</summary>
    public void Stop() => _running = false;

    /// <summary>Runs the read-eval-print loop until the shell is stopped.</summary>
    public void Run()
    {
        while (_running)
        {
            Console.Write("medli~> ");
            string? line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                continue;
            Parse(line);
        }
    }

    private void Parse(string line)
    {
        int index = line.IndexOf(' ');
        string command;
        string param;
        if (index == -1)
        {
            command = line;
            param = "";
        }
        else
        {
            command = line.Substring(0, index);
            param = line.Substring(index + 1);
        }

        foreach (var c in _commands)
        {
            if (c.Name == command)
            {
                c.Execute(param);
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The command " + command + " is not supported. Please type help for more information.");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
