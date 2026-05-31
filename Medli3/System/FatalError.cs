using System;
using Cosmos.Kernel.System;

namespace Medli;

/// <summary>
/// The kernel panic screen. Ported from Medli/Common/FatalError.cs (slimmed): when the
/// shell loop throws, the kernel routes the exception here instead of letting it unwind
/// and freeze the machine. Shows the error, then reboots on a key press.
/// </summary>
public static class FatalError
{
    private const string ErrorSplash =
@"A fatal error has occurred and Medli was shut down to protect your computer
from further damage. If this is the first time you've seen this error, press
any key to restart. This error may have been caused by newly installed or
older failing hardware.

Error information can be found below:";

    /// <summary>Shows the panic screen for an exception, then waits and reboots.</summary>
    public static void Crash(Exception ex)
    {
        Crash("Unhandled exception", ex.Message);
    }

    /// <summary>Shows the panic screen for a message/description, then waits and reboots.</summary>
    public static void Crash(string error, string description)
    {
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine("                          *** KERNEL PANIC ***");
        Console.WriteLine();
        Console.WriteLine(ErrorSplash);
        Console.WriteLine();
        Console.WriteLine("Kernel version:  Medli " + Kernel.KernelVersion + " (build " + MedliInfo.BuildNumber + ")");
        Console.WriteLine("Error:           " + error);
        Console.WriteLine("Description:     " + description);
        Console.WriteLine();
        Console.WriteLine("The system has halted. Reset the machine to restart.");

        // A panic is terminal: park the CPU and keep the screen up indefinitely, rather
        // than rebooting (gen3 key input isn't reliable enough to wait on, and a kernel
        // panic should stay visible). HLT-and-loop so a stray interrupt can't run on.
        while (true)
            Power.Halt();
    }
}
