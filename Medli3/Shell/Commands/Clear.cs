using System;

namespace Medli.Apps;

/// <summary>The 'cls' command — clears the screen. Ported from Medli/Kernel/Utils/Shell/Clear.cs.</summary>
public class Clear : Command
{
    public override string Name => "cls";
    public override string Summary => "Clears the screen.";

    public override void Execute(string? param) => Console.Clear();
}
