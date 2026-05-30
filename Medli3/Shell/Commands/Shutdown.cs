using System;

namespace Medli.Apps;

/// <summary>
/// The 'shutdown' command. Ported from Medli/Kernel/Utils/System/Shutdown.cs.
/// Asks the shell to stop; the kernel performs the actual power-off once the
/// shell loop returns. (The Medli Legacy version also called EnvironmentVariables.SaveVars(),
/// deferred until the filesystem is ported.)
/// </summary>
public class Shutdown : Command
{
    private readonly Action _stop;

    public Shutdown(Action stop) => _stop = stop;

    public override string Name => "shutdown";
    public override string Summary => "Closes applications and powers down the system.";

    public override void Execute(string? param)
    {
        // EnvironmentVariables.SaveVars();   // FS not ported yet
        Console.WriteLine("Shutting down...");
        _stop();
    }
}
