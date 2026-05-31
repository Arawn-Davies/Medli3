using System;
using System.Collections.Generic;
using Medli.Apps;

namespace Medli;

/// <summary>
/// The interactive shell. Slimmed port of Medli/Kernel/CommandConsole.cs: keeps the
/// command registry + parser, but drops the Medli Legacy login/screen/filesystem state and
/// only registers commands that have been ported so far (no IO yet).
/// </summary>
public class CommandConsole
{
    private readonly List<Command> _commands = new List<Command>();
    private bool _running = true;

    public CommandConsole()
    {
        // --- Ported, working commands (console / pure logic) ---
        _commands.Add(new Clear());
        _commands.Add(new Echo());
        _commands.Add(new Set());
        _commands.Add(new Get());
        _commands.Add(new Apps.Date());
        _commands.Add(new Apps.Time());
        _commands.Add(new Apps.Version());
        _commands.Add(new Cowsay());
        _commands.Add(new ColorChanger());
        _commands.Add(new Pause());
        _commands.Add(new Md5Command());
        _commands.Add(new Sha256Command());
        _commands.Add(new Reboot());
        _commands.Add(new Shutdown(Stop));
        _commands.Add(new Logout());
        _commands.Add(new Panic());

        // --- Stubs: real Medli commands whose subsystem isn't ported to gen3 yet.
        //     They keep their name/summary so `help` shows the full Medli surface, and
        //     explain themselves when run. Replace each with a real port as it lands. ---

        // Filesystem (needs the FS subsystem)
        _commands.Add(new StubCommand("dir", "Lists the files in the current directory.", StubReason.Filesystem));
        _commands.Add(new StubCommand("cd", "Changes to the specified directory.", StubReason.Filesystem));
        _commands.Add(new StubCommand("mkdir", "Makes a directory using the specified name.", StubReason.Filesystem));
        _commands.Add(new StubCommand("rm", "Removes the specified file.", StubReason.Filesystem));
        _commands.Add(new StubCommand("copy", "Copies the specified file to the specified destination.", StubReason.Filesystem));
        _commands.Add(new StubCommand("move", "Moves the specified file to the specified directory.", StubReason.Filesystem));
        _commands.Add(new StubCommand("run", "Runs the specified script file (ending in .mds).", StubReason.Filesystem));
        _commands.Add(new StubCommand("cpview", "Prints the contents of a file onto the screen.", StubReason.Filesystem));
        _commands.Add(new StubCommand("cpedit", "Launches the CocoaPad text editor.", StubReason.Filesystem));

        // Applications (not ported yet)
        _commands.Add(new StubCommand("vics", "The VICS (vi C-Sharp) full-screen text editor.", StubReason.Application));
        _commands.Add(new StubCommand("launch", "Launches an .ma Medli Application.", StubReason.Application));
        _commands.Add(new StubCommand("screen", "Switches the current terminal to a new multiscreen terminal.", StubReason.Application));
        _commands.Add(new StubCommand("devenv", "Launches the development environment.", StubReason.Application));

        // Disk/hardware (needs disk drivers)
        _commands.Add(new StubCommand("fdisk", "Launches the disk utility.", StubReason.Hardware));

        // Help is last so it can list every other command.
        _commands.Add(new HelpCommand(_commands));
    }

    /// <summary>Stops the shell loop (used by the 'shutdown' command).</summary>
    public void Stop() => _running = false;

    /// <summary>Runs the read-eval-print loop until the shell is stopped.</summary>
    public void Run()
    {
        while (_running)
        {
            // Prompt mirrors the Medli Legacy shell: user@host currentdir~>
            Console.Write(Kernel.username + "@" + Kernel.pcname + " " + Paths.CurrentDirectory + "~> ");
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
                // Catch at the call site (one frame above the throw): any exception a
                // command raises is a fatal fault → show the kernel panic screen. gen3's
                // exception unwinding is shallow, so catching here (rather than up in
                // Kernel.Run) is what reliably reaches the handler.
                try
                {
                    c.Execute(param);
                }
                catch (Exception ex)
                {
                    Medli.FatalError.Crash(ex);
                }
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The command " + command + " is not supported. Please type help for more information.");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
