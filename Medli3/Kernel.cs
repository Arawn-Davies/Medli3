using System;
using Cosmos.Kernel.System;
using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
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
        // Swap the bundled 16x32 DefaultFont for a smaller embedded 8x16 font, so wide
        // output (e.g. the colour spectrum's name row) fits on one line instead of
        // wrapping. Done before Clear() so the console recomputes its rows/columns for
        // the new glyph size. (The Cosmos resource-name font hook is assembly-scoped to
        // Cosmos.Kernel.System and can't see our resource, so we load the bytes here.)
        var console = KernelConsole.Default;
        if (console != null)
        {
            console.Font = PCScreenFont.LoadFont(Medli.Spleen8x16Font.Data);
        }

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
        Console.WriteLine("Type 'help' for a list of commands, 'shutdown' to power off.");
        Console.WriteLine();
    }

    protected override void Run()
    {
        // The shell owns its own read-eval-print loop and returns when 'shutdown' runs.
        // Any exception that escapes the shell (e.g. the 'panic' command) is a fatal
        // kernel fault: show the panic screen instead of unwinding into a frozen CPU.
        try
        {
            _shell.Run();
        }
        catch (Exception ex)
        {
            Medli.FatalError.Crash(ex);   // shows the panic screen, then reboots
            return;
        }

        Console.WriteLine("Shutting down...");
        Power.Shutdown();
    }
}
