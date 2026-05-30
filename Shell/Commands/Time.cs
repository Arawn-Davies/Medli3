using System;

namespace Medli.Apps;

/// <summary>The 'time' command. Prints the current time via <see cref="Medli.Clock"/>.</summary>
public class Time : Command
{
    public override string Name => "time";
    public override string Summary => "Displays the current time.";

    public override void Execute(string? param) => Clock.PrintTime();
}
