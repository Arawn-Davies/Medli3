using System;

namespace Medli.Apps;

/// <summary>
/// The 'colour' command — changes the fore/background colour of the terminal.
/// Ported from Medli/Kernel/Utils/Applications/Colour.cs. Adapted for Medli3: after
/// changing a colour it calls <see cref="Medli.Kernel.SaveColourScheme"/> so the choice
/// is persisted and survives a `cls`/scroll (Cosmos gen3 KernelConsole doesn't keep a
/// per-cell background across redraws, so the scheme must be the source of truth).
/// </summary>
public class ColorChanger : Command
{
    public override string Name => "colour";
    public override string Summary => "Changes the back/foreground colour of the terminal (colour bg 0-15 | fg a-p).";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
        {
            Console.WriteLine("Usage: colour bg <0-15>  |  colour fg <a-p>");
            return;
        }

        string[] args = param.Split(' ');
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: colour bg <0-15>  |  colour fg <a-p>");
            return;
        }

        if (args[0] == "bg")
            ChangeBGC(args[1]);
        else if (args[0] == "fg")
            ChangeFGC(args[1]);
        else
            Console.WriteLine("Usage: colour bg <0-15>  |  colour fg <a-p>");

        // Persist whatever was set so it survives clears/scrolls.
        Medli.Kernel.SaveColourScheme();
    }

    public static void ChangeBGC(string color)
    {
        switch (color)
        {
            case "1": Console.BackgroundColor = ConsoleColor.DarkRed; break;
            case "2": Console.BackgroundColor = ConsoleColor.DarkBlue; break;
            case "3": Console.BackgroundColor = ConsoleColor.DarkCyan; break;
            case "4": Console.BackgroundColor = ConsoleColor.DarkMagenta; break;
            case "5": Console.BackgroundColor = ConsoleColor.DarkGreen; break;
            case "6": Console.BackgroundColor = ConsoleColor.DarkGray; break;
            case "7": Console.BackgroundColor = ConsoleColor.Red; break;
            case "8": Console.BackgroundColor = ConsoleColor.Blue; break;
            case "9": Console.BackgroundColor = ConsoleColor.Cyan; break;
            case "10": Console.BackgroundColor = ConsoleColor.Magenta; break;
            case "11": Console.BackgroundColor = ConsoleColor.Green; break;
            case "12": Console.BackgroundColor = ConsoleColor.Gray; break;
            case "13": Console.BackgroundColor = ConsoleColor.Yellow; break;
            case "14": Console.BackgroundColor = ConsoleColor.DarkYellow; break;
            case "15": Console.BackgroundColor = ConsoleColor.Black; break;
            case "0": Console.BackgroundColor = ConsoleColor.White; break;
            default: Console.WriteLine("Invalid colour, valid options are 0 - 15"); break;
        }
    }

    public static void ChangeFGC(string color)
    {
        switch (color)
        {
            case "a": Console.ForegroundColor = ConsoleColor.DarkRed; break;
            case "b": Console.ForegroundColor = ConsoleColor.DarkBlue; break;
            case "c": Console.ForegroundColor = ConsoleColor.DarkCyan; break;
            case "d": Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
            case "e": Console.ForegroundColor = ConsoleColor.DarkGreen; break;
            case "f": Console.ForegroundColor = ConsoleColor.DarkGray; break;
            case "g": Console.ForegroundColor = ConsoleColor.Red; break;
            case "h": Console.ForegroundColor = ConsoleColor.Blue; break;
            case "i": Console.ForegroundColor = ConsoleColor.Cyan; break;
            case "j": Console.ForegroundColor = ConsoleColor.Magenta; break;
            case "k": Console.ForegroundColor = ConsoleColor.Green; break;
            case "l": Console.ForegroundColor = ConsoleColor.Gray; break;
            case "m": Console.ForegroundColor = ConsoleColor.Yellow; break;
            case "n": Console.ForegroundColor = ConsoleColor.DarkYellow; break;
            case "o": Console.ForegroundColor = ConsoleColor.Black; break;
            case "p": Console.ForegroundColor = ConsoleColor.White; break;
            default: Console.WriteLine("Invalid colour, valid options are a - p"); break;
        }
    }
}
