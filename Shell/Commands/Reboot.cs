using System;
using Cosmos.Kernel.System;

namespace Medli.Apps;

/// <summary>The 'reboot' command. Ported from Medli/Kernel/Utils/System/Reboot.cs.</summary>
public class Reboot : Command
{
    public override string Name => "reboot";
    public override string Summary => "Closes applications and reboots the system.";

    public override void Execute(string? param)
    {
        // EnvironmentVariables.SaveVars();   // FS not ported yet
        Console.WriteLine("Rebooting...");
        Power.Reboot();   // [DoesNotReturn]
    }
}
