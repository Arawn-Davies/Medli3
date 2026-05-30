using System;
using Cosmos.Kernel.System;
using Sys = Cosmos.Kernel.System;

namespace Medli3;

/// <summary>
/// Main kernel class - inherits from Cosmos.Kernel.System.Kernel.
/// </summary>
public class Kernel : Sys.Kernel
{
    private readonly Medli.CommandConsole _shell = new Medli.CommandConsole();

    protected override void BeforeRun()
    {
        // White-on-blue colour scheme, like the Medli Legacy boot (Init/Boot.cs).
        Medli.Kernel.backgroundColour = ConsoleColor.Blue;
        Medli.Kernel.foregroundColour = ConsoleColor.White;
        Medli.Kernel.SetColourScheme();
        Console.Clear();

        // Startup screen: ASCII logo + welcome banner.
        Console.WriteLine(Medli.Kernel.Logo);
        Console.WriteLine(Medli.Kernel.Welcome);

        // Full colour spectrum (also re-applies white-on-blue afterwards).
        Medli.Spectrum.Show();
        Medli.Kernel.SetColourScheme();

        Console.WriteLine();
        Console.WriteLine("Type 'help' for a list of commands, 'exit' to shut down.");
        Console.WriteLine();
    }

    protected override void Run()
    {
        // The shell owns its own read-eval-print loop and returns when 'exit' runs.
        _shell.Run();
        Console.WriteLine("Shutting down...");
        Power.Shutdown();
    }
}
