using System;

namespace Medli.Apps;

/// <summary>
/// The 'pause' command — prompts the user to press any key. Ported from
/// Medli/Kernel/Utils/Shell/Pause.cs (which delegated to KernelExtensions.PressAnyKey);
/// here it uses the Cosmos gen3 <c>Console.ReadKey()</c> directly.
/// </summary>
public class Pause : Command
{
    public override string Name => "pause";
    public override string Summary => "Prompts the user to press any key.";

    public override void Execute(string? param)
    {
        Console.WriteLine(string.IsNullOrEmpty(param) ? "Press any key to continue . . ." : param);
        Console.ReadKey();
    }
}
