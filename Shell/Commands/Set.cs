using System;

namespace Medli.Apps;

/// <summary>
/// The 'set' command (invoked as <c>$</c>). Ported from Medli/Kernel/Utils/Shell/Set.cs.
/// The save/load subcommands go through the filesystem and are deferred.
/// </summary>
public class Set : Command
{
    public override string Name => "$";
    public override string Summary => "Sets a variable: $ [name] [value]  (append -u to overwrite).";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
            return;

        if (param == "print")
        {
            EnvironmentVariables.PrintVars();
            return;
        }
        // save/load deferred (filesystem):
        // if (param == "save") { EnvironmentVariables.SaveVars(); return; }
        // if (param == "load") { EnvironmentVariables.ReadVars(); return; }

        // Split into name + value on the first space (guarded against a missing value).
        int sp = param.IndexOf(' ');
        string name = sp == -1 ? param : param.Substring(0, sp);
        string value = sp == -1 ? "" : param.Substring(sp + 1);
        bool force = value == "-u" || value.EndsWith(" -u");

        EnvironmentVariables.Store(name, value, force);
    }
}
