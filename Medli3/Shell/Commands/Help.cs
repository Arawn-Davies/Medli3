using System;
using System.Collections.Generic;

namespace Medli.Apps;

/// <summary>
/// The 'help' command. Ported from Medli/Kernel/Utils/Shell/Help.cs.
/// `help` lists every command (name + one-line description); `help &lt;command&gt;` shows
/// that command's man page. Kept deliberately simple — plain aligned text, no table
/// chrome (and ASCII only: Cosmos' KernelConsole renders a glyph per UTF-8 byte, so
/// multi-byte box-drawing glyphs would come out as garbage).
/// </summary>
public class HelpCommand : Command
{
    private const int NameWidth = 12;   // longest name ("shutdown") + slack

    private readonly List<Command> _commands;

    public HelpCommand(List<Command> commands) => _commands = commands;

    public override string Name => "help";
    public override string Summary => "Lists commands, or shows the man page for one (help <command>).";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
            DisplayCommands();
        else
            ManPage(param);
    }

    // The command index: one line per command, "name  short description".
    private void DisplayCommands()
    {
        foreach (var c in _commands)
        {
            Console.Write("  ");
            Console.Write(Pad(c.Name, NameWidth));
            Console.WriteLine(FirstLine(c.Summary));
        }
        Console.WriteLine();
        Console.WriteLine("Type 'help <command>' for the full man page.");
        Console.WriteLine();
    }

    // A minimal man page for one command: NAME + DESCRIPTION (the full summary).
    private void ManPage(string command)
    {
        foreach (var c in _commands)
        {
            if (c.Name == command)
            {
                Console.WriteLine("NAME");
                Console.WriteLine("    " + c.Name + " - " + FirstLine(c.Summary));
                Console.WriteLine();
                Console.WriteLine("DESCRIPTION");
                foreach (var line in c.Summary.Split('\n'))
                    Console.WriteLine("    " + line.TrimEnd('\r'));
                Console.WriteLine();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No manual entry for " + command + ". Type 'help' for the list.");
        Medli.Kernel.SetColourScheme();
    }

    /// <summary>The first line of a (possibly multi-line) summary.</summary>
    private static string FirstLine(string summary)
    {
        int nl = summary.IndexOf('\n');
        return (nl == -1 ? summary : summary.Substring(0, nl)).TrimEnd('\r', ' ');
    }

    /// <summary>Right-pads <paramref name="s"/> with spaces to <paramref name="width"/>.</summary>
    private static string Pad(string s, int width)
    {
        if (s.Length >= width)
            return s + " ";
        return s + new string(' ', width - s.Length);
    }

    public override void Help() => ManPageSelf();

    private void ManPageSelf()
    {
        Console.WriteLine("NAME");
        Console.WriteLine("    help - " + FirstLine(Summary));
        Console.WriteLine();
        Console.WriteLine("DESCRIPTION");
        Console.WriteLine("    help            Lists every command with a short description.");
        Console.WriteLine("    help <command>  Shows the man page for that command.");
        Console.WriteLine();
    }
}
