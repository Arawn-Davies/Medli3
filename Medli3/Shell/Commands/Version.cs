using System;

namespace Medli.Apps;

/// <summary>
/// The 'version' command. Ported from Medli/Kernel/Utils/Shell/Version.cs.
/// Class name is also `Version`; reference it as `Apps.Version` to avoid clashing
/// with System.Version.
/// </summary>
public class Version : Command
{
    public override string Name => "version";
    public override string Summary => "Displays version information about Medli.";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param) || param == "ver")
            DisplayVersion();
        else
            Help();
    }

    private void DisplayVersion()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Medli.MedliInfo.Copyright);
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Medli3 v" + Medli.MedliInfo.Version + " (Medli gen3, on Cosmos gen3 / NativeAOT)");
        Console.WriteLine("Build: " + Medli.MedliInfo.BuildNumber);
        Console.WriteLine("Built against Cosmos v" + Medli.MedliInfo.CosmosVersion);
        Console.WriteLine();
    }

    public override void Help()
    {
        Console.WriteLine("version [ver]");
        Console.WriteLine("  " + Summary);
    }
}
