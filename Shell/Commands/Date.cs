using System;

namespace Medli.Apps;

/// <summary>The 'date' command. Prints the current date via <see cref="Medli.Clock"/>.</summary>
public class Date : Command
{
    public override string Name => "date";
    public override string Summary => "Displays the current date.";

    public override void Execute(string? param) => Clock.PrintDate();
}
