using System;

namespace Medli.Apps;

/// <summary>
/// The 'echo' command — prints its argument back to the console.
/// Ported from Medli/Kernel/Utils/Shell/Echo.cs, including the `$name` lookup that
/// prints an environment variable's value.
/// </summary>
public class Echo : Command
{
    public override string Name => "echo";
    public override string Summary => "Duplicates text you enter to the console ($name prints a variable).";

    public override void Execute(string? param)
    {
        if (param != null && param.StartsWith("$"))
            Console.WriteLine(EnvironmentVariables.Retrieve(param.Substring(1)));
        else
            Console.WriteLine(param);
    }
}
